using System.ComponentModel.DataAnnotations;

namespace AdminPanelMVC.Models.AboutMe;

public class CreateAboutMeViewModel
{
    [Required]
    public string Introduction { get; set; } = string.Empty;

	[Required]
	public IFormFile Image1 { get; set; }

	[Required]
	public IFormFile Image2 { get; set; }

}
