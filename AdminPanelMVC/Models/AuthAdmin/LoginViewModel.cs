using System.ComponentModel.DataAnnotations;

namespace AdminPanelMVC.Models.AuthAdmin;

public class LoginViewModel
{
    [Required, MaxLength(50), EmailAddress]
    public string Email { get; set; }

    [Required, MaxLength(50), MinLength(8), DataType(DataType.Password)]
    public string Password { get; set; }

}
