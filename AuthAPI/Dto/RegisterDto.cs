using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Dto
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public EmailAddressAttribute Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required,Compare(nameof(Password)), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
