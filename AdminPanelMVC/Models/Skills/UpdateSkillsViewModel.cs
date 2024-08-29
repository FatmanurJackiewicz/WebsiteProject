using System.ComponentModel.DataAnnotations;
namespace AdminPanelMVC.Models.Skills
{
    
    public class UpdateSkillsViewModel
    {
        [Required]
        public string Description { get; set; }
        public List<string> SkillsList { get; set; } // Skills listesi buraya eklenmiş olmalı
    }
}
