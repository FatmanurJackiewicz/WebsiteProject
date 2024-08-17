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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            var user = new User()
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password = registerDto.Password,
                ConfirmPassword = registerDto.ConfirmPassword
                
            };

            _users.Add(user);

            return Ok(_users);
        }

    }
}
