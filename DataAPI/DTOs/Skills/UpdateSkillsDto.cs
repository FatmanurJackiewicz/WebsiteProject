using System.ComponentModel.DataAnnotations;

namespace DataAPI.DTOs.Skills
{
    public class UpdateSkillsDto
	{
        [Required]
        public List<string> SkillsList { get; set; }
    }
}
