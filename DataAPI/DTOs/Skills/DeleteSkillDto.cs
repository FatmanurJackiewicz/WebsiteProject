using System.ComponentModel.DataAnnotations;

namespace DataAPI.DTOs.Skills
{
    public class DeleteSkillDto
    {
        [Required]
        public string SkillName { get; set; }
    }
}
