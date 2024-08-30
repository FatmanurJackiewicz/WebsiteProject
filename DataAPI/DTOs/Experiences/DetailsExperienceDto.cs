using System.ComponentModel.DataAnnotations;

namespace DataAPI.DTOs.Experiences;

public class DetailsExperienceDto
{

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Company { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;
}
