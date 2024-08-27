using System.ComponentModel.DataAnnotations;

namespace AdminPanelMVC.Models.AuthAdmin;

public class RenewPasswordViewModel
{
    [Required]
    public string VerificationCode { get; set; } = null!;

    [Required, MaxLength(50), MinLength(8), DataType(DataType.Password)]
    public string NewPassword { get; set; } = null!;
}
