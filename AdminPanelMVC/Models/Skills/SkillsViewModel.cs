using System.ComponentModel.DataAnnotations;

namespace AdminPanelMVC.Models.Skills
{
    public class SkillsViewModel
    {
        [Required]
        public string Description { get; set; }
    }
}
