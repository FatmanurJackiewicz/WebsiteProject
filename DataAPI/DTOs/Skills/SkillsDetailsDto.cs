using System.ComponentModel.DataAnnotations;

namespace DataAPI.DTOs.Skills
{
	public class SkillsDetailsDto
	{
		[Required]
		public string Description { get; set; }
        public List<string> SkillsList { get; set; }
    }
}
