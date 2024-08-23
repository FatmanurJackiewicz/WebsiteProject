using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Dto;

public class ForgotPasswordRequestDto
{
    [Required]
    public string Email { get; set; } = null!;
}
