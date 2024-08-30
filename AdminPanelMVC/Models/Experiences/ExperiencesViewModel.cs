using AuthAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace AdminPanelMVC.Models.Experiences
{
    public class ExperiencesViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
