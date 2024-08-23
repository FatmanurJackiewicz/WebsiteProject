﻿using AuthAPI.Dto;
using AuthAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentValidation;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {        
        private static AppDbContext _appDbContext;
        private static IConfiguration _configuration;

        public AuthController(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        [HttpGet("UserList")]
        public IActionResult GetUsers()
        {
            var userList = _appDbContext.Users
                .Include(u => u.Role)
                .Include(u => u.RefreshTokens)
                .Select(u => new {
                    u.Id,
                    u.Username,
                    u.Email,
                    RoleName = u.Role.Name,
                    u.RoleId,
                    RefreshTokens = u.RefreshTokens.Select(x => x.Token)
                })
                .ToList();
            return Ok(userList);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }

            var loginPasswordHash = HashPassword(dto.Password);

            var user = _appDbContext.Users.SingleOrDefault(u => u.Email == dto.Email && u.PasswordHash == loginPasswordHash);

            if (user == null)
            {
                return BadRequest("Username or Password is wrong.");
            }

            if(_appDbContext.RefreshTokens.Any())
            {
                var list = _appDbContext.RefreshTokens.Where(u => u.UserId == user.Id).ToList();
                list.ForEach(t => t.IsRevoked = true);
                //user.RefreshTokens.ForEach(t => t.IsRevoked = true);
            }
            
            _appDbContext.SaveChanges();


            var jwt = GenerateJwtToken(user);
            string refreshToken = GenerateRefreshToken();

            var refreshTokenModel = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            };

            _appDbContext.RefreshTokens.Add(refreshTokenModel);
            _appDbContext.SaveChanges();

            var tokenResponse = new TokenResponseDto
            {
                AccessToken = jwt,
                RefreshToken = refreshToken
            };

            if (tokenResponse is null)
            {
                return BadRequest("Token oluşturulamadı");
            }
            else
            {
                HttpContext.Response.Cookies.Append("auth-cookie", tokenResponse.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = refreshTokenModel.ExpiryDate
                });
            }

            return Ok(tokenResponse);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto, [FromServices] IValidator<RegisterDto> dtoValidator)
        {
            var validationResult = dtoValidator.Validate(registerDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                return BadRequest(errorMessages);
            }

            var userExists = _appDbContext.Users.Any(u => u.Email == registerDto.Email);
            if (userExists)
            {
                return BadRequest("A user with this email already exists.");
            }

            var passwordHash = HashPassword(registerDto.Password);

            var user = new User()
            {
                Username = registerDto.Username,
                Email = registerDto.Email.ToLower(), // Normalize email case
                PasswordHash = passwordHash,
                RoleId = 2
            };

            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();

            return Ok();

        }


        [HttpGet("Refresh")]
        public IActionResult Refresh([FromQuery] string token)
        {
            var refreshToken = _appDbContext.RefreshTokens
                            .Include(x => x.User)
                            .SingleOrDefault(x =>
                                x.Token == token &&
                                x.ExpiryDate > DateTime.UtcNow &&
                                x.IsRevoked == false);

            if (refreshToken is null)
                return Unauthorized("Invalid refresh token");


            if (refreshToken.IsUsed)
                return Unauthorized("Invalid refresh token");


            refreshToken.IsUsed = true;

            var jwt = GenerateJwtToken(refreshToken.User);
            var newRefreshToken = GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                UserId = refreshToken.User.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            };
            _appDbContext.RefreshTokens.Add(refreshTokenEntity);
            _appDbContext.SaveChanges();

            var tokenResponse = new TokenResponseDto
            {
                AccessToken = jwt,
                RefreshToken = newRefreshToken
            };


            return Ok(tokenResponse);
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto dto)
        {
            if (dto is null) //validasyon mu? business rule mu?
                return BadRequest("DTO is null");

            var user = await _appDbContext.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            var resetPasswordToken = Guid.NewGuid().ToString("n");

            user.ResetPasswordToken = resetPasswordToken;
            _appDbContext.Users.Update(user);
            _appDbContext.SaveChanges();

            await SendResetPasswordEmailAsync(user);

            return Ok("Password reset email has been sent. Please check your email.");
        }

        [HttpPost("renew-password")]
        public async Task<IActionResult> RenewPassword([FromBody] RenewPasswordRequestDto dto)
        {
            //validasyon mu business rule mu?
            if (dto is null || string.IsNullOrEmpty(dto.VerificationCode) || string.IsNullOrEmpty(dto.NewPassword))
            {
                return BadRequest("Invalid request.");
            }

            var user = await _appDbContext.Users.SingleOrDefaultAsync(u => u.ResetPasswordToken == dto.VerificationCode);

            if (user == null)
            {
                return NotFound("Invalid verification code.");
            }

            var newPasswordHash = HashPassword(dto.NewPassword);
            user.PasswordHash = newPasswordHash;
            user.ResetPasswordToken = null; // Tokeni sıfırla

            _appDbContext.Users.Update(user);
            _appDbContext.SaveChanges();

            return Ok("Password has been successfully reset.");
        }


        public static byte[] HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private async Task SendResetPasswordEmailAsync(User user)
        {
            const string host = "smtp.gmail.com";
            const int port = 587;
            const string from = "denemebackend105@gmail.com";
            const string password = "boyc tvfg jkgp thpk";

            using SmtpClient client = new(host, port)
            {
                Credentials = new NetworkCredential(from, password),
                EnableSsl = true
            };

            MailMessage mail = new()
            {
                From = new MailAddress(from),
                Subject = "Şifre Sıfırlama",
                Body = $"Merhaba {user.Username}, <br> Şifrenizi sıfırlamak için validasyon kodu '{user.ResetPasswordToken}'",
                IsBodyHtml = true,
            };

            mail.To.Add(user.Email);

            await client.SendMailAsync(mail);
        }
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                //new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpireMinutes")),
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);

            return tokenString;
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
