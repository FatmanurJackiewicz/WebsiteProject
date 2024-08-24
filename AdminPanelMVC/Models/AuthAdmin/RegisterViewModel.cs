using System.ComponentModel.DataAnnotations;

namespace AdminPanelMVC.Models.AuthAdmin;

public class RegisterViewModel
{
	[Required]
	public string Username { get; set; }

	[Required, MaxLength(50), DataType(DataType.EmailAddress)]
	public string Email { get; set; }

	[Required, MaxLength(50), DataType(DataType.Password)]
	public string Password { get; set; }
}
