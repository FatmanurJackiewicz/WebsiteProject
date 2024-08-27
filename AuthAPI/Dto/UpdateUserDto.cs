using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Dto;

public class UpdateUserDto
{
    [Required]
    public string Username { get; set; }

    [Required, MaxLength(50), EmailAddress]
    public string Email { get; set; }

    public string OldEmail { get; set; }
}
