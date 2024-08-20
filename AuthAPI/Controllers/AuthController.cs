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

        [HttpPost]
        public IActionResult Login([FromForm] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            var user = _users.Find(u => u.Email == dto.Email && u.Password == login.Password);

            if (user == null)
            {
                return BadRequest("Username or Password is wrong.");
            }



        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            var user = new User()
            {
                Username = registerDto.Username,
                Password = registerDto.Password                
            };

            _users.Add(user);

            return Ok(_users);
        }

    }
}
