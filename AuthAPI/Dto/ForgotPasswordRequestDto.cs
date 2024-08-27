using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Dto;

public class ForgotPasswordRequestDto
{
    [Required, MaxLength(50), EmailAddress]
    public string Email { get; set; } = null!;
}
