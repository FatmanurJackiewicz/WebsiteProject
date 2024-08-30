using System.ComponentModel.DataAnnotations;

namespace AdminPanelMVC.Models.Skills
{
	public class CreateSkillsViewModel
	{

		[Required]
		public List<string> SkillsList { get; set; }
	}
}
