using System.ComponentModel.DataAnnotations;
namespace AdminPanelMVC.Models.Skills
{
    
    public class UpdateSkillsViewModel
    {
        [Required]
        public List<string> SkillsList { get; set; }
    }
}
