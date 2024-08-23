using DataAPI.Data;
using DataAPI.DTOs.Projects;
using DataAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public ProjectsController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject([FromRoute] int id)
    {
        var project = await _appDbContext.Projects.FindAsync(id);

        if (project is null)
            return NotFound("Project not found");

        return Ok(project);
    }

    [HttpPost("createProjects")]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectsDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newProject = new Projects
        {
            Title = createDto.Title,
            Description = createDto.Description,
            ImageUrl = createDto.ImageUrl
        };

        _appDbContext.Projects.Add(newProject);
        await _appDbContext.SaveChangesAsync();

        return Ok(newProject);
    }

    [HttpPut("updateProjects/{id}")]
    public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectsDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existProject = await _appDbContext.Projects.FindAsync(updateDto.Id);

        if (existProject is null)
            return NotFound("Project not found");

        existProject.Title = updateDto.Title;
        existProject.Description = updateDto.Description;
        existProject.ImageUrl = updateDto.ImageUrl;

        _appDbContext.Projects.Update(existProject);
        await _appDbContext.SaveChangesAsync();

        return Ok(existProject);
    }

    [HttpDelete("deleteProjects/{id}")]
    public async Task<IActionResult> DeleteProject([FromRoute] int id)
    {
        var existProject = await _appDbContext.Projects.FindAsync(id);

        if (existProject is null)
        {
            return NoContent();
        }

        _appDbContext.Projects.Remove(existProject);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }
}
