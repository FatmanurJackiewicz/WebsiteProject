using System.ComponentModel.DataAnnotations;

namespace AdminPanelMVC.Models.Profile
{
    public class UserViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required, MaxLength(50), EmailAddress]
        public string Email { get; set; }
    }
}
