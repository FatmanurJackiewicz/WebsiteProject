using System.ComponentModel.DataAnnotations;

namespace PortfoyMVC.Models;

public class SkillsPortfoyViewModel
{
    [Required]
    public List<string> SkillsList { get; set; }
}