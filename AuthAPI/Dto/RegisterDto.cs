using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Dto
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
