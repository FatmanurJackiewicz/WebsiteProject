using AuthAPI.Dto;
using AuthAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static readonly List<User> _users = new();

        [HttpGet("UserList")]
        public IActionResult GetUsers()
        {
            return Ok(_users);
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            var user = _users.Find(u => u.Email == dto.Email && u.Password == dto.Password);

            if (user == null)
            {
                return BadRequest("Username or Password is wrong.");
            }

            return Ok("Login is successful");
        }

        [HttpPost("register")]
        public IActionResult Register([FromForm] RegisterDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            var userExists = _users.Any(u => u.Email == registerDto.Email);
            if (userExists)
            {
                return BadRequest("A user with this email already exists.");
            }

            var user = new User()
            {
                Username = registerDto.Username,
                Email = registerDto.Email.ToLower(), // Normalize email case
                Password = registerDto.Password,
            };

            _users.Add(user);

            return Ok();

        }
    }
}
