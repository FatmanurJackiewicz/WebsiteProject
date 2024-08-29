using System.ComponentModel.DataAnnotations;

namespace AdminPanelMVC.Models.AboutMe;

public class AboutMeViewModel
{
	public int Id { get; set; }
	[Required]
	public string Introduction { get; set; } = string.Empty;

	[Required]
	public string Image1Url { get; set; }

	[Required]
	public string Image2Url { get; set; }

}
