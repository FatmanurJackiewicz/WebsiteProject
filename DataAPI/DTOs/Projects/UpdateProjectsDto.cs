using System.ComponentModel.DataAnnotations;

namespace DataAPI.DTOs.Projects;

public class UpdateProjectsDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string ImageUrl { get; set; } = string.Empty;
}
