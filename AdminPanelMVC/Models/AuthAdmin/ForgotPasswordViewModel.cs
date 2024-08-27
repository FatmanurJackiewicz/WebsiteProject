using System.ComponentModel.DataAnnotations;

namespace AdminPanelMVC.Models.AuthAdmin;

public class ForgotPasswordViewModel
{
    [Required, MaxLength(50), EmailAddress]
    public string Email { get; set; }
}
