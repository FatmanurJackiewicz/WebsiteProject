using System.ComponentModel.DataAnnotations;

namespace AdminPanelMVC.Models.Skills
{
    public class SkillsViewModel
    {
        [Required]
        public List<string> SkillsList { get; set; }
    }
}
