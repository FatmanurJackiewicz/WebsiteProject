using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Dto
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required, MaxLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, MaxLength(50), DataType(DataType.Password)]
        public string Password { get; set; }


        [Compare(nameof(Password)), DataType(DataType.Password)]
        public string ConfirmPassword { get; internal set; }

    }
}
