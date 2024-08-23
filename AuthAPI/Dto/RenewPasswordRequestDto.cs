using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Dto;

public class RenewPasswordRequestDto
{
    [Required]
    public string VerificationCode { get; set; } = null!;

    [Required]
    public string NewPassword { get; set; } = null!;
}
