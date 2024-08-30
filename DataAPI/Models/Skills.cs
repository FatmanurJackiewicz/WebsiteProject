using System.ComponentModel.DataAnnotations;

namespace DataAPI.Models
{
	public class Skills
	{
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
