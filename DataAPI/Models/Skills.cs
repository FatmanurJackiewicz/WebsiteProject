using System.ComponentModel.DataAnnotations;

namespace DataAPI.Models
{
	public class Skills
	{
		[Required]
		public string Description { get; set; }
        public List<string> SkillsList { get; set; }
    }
}
