using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Dto;

public class LoginDto
{
    [Required, MaxLength(50), DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required, MaxLength(50), DataType(DataType.Password)]
    public string Password { get; set; }

    public string Project { get; set; }

}
