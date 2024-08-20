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

        [HttpPost ("login")]
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

            return Ok("Login is succesful");


        }

        [HttpPost("register")]
        public IActionResult Register([FromForm] RegisterDto registerDto)
        {
            var user = new User()
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password = registerDto.Password,
            };
            var ConfirmPassword = registerDto.ConfirmPassword;

            _users.Add(user);

            return Ok(_users);
        }

    }
}
