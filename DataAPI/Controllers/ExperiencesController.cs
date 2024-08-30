using DataAPI.Data;
using DataAPI.DTOs.Experiences;
using DataAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExperiencesController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public ExperiencesController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet()]
    public async Task<IActionResult> GetExperiences()
    {
        var experience = _appDbContext.Experiences.FirstOrDefault();

        if (experience is null)
            return NotFound("Experience not found");

        var detailsExperienceDto = new DetailsExperienceDto
        {
            Title = experience.Title,
            Company = experience.Company,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Description = experience.Description
        };

        return Ok(detailsExperienceDto);
    }

    
    [HttpPost("createExperiences")]
    public async Task<IActionResult> CreateExperiences([FromBody] CreateExperienceDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //startDate ve endDate geç olamama durumu - business rule

        var newExperiences = new Experiences
        {
            Title = createDto.Title,
            Company = createDto.Company,
            StartDate = createDto.StartDate,
            EndDate = createDto.EndDate,
            Description = createDto.Description
        };

        _appDbContext.Experiences.Add(newExperiences);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

    
    [HttpPost("updateExperiences")]
    public async Task<IActionResult> UpdateExperiences([FromBody] UpdateExperienceDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existExperiences = _appDbContext.Experiences.FirstOrDefault();

        if (existExperiences is null)
            return NotFound("Experience not found");

        existExperiences.Title = updateDto.Title;
        existExperiences.Company = updateDto.Company;
        existExperiences.StartDate = updateDto.StartDate;
        existExperiences.EndDate = updateDto.EndDate;
        existExperiences.Description = updateDto.Description;

        _appDbContext.Experiences.Update(existExperiences);
        await _appDbContext.SaveChangesAsync();

        return Ok(existExperiences);
    }


    [HttpDelete("deleteExperiences/{id}")]
    public async Task<IActionResult> DeleteExperience([FromRoute] int id)
    {
        var existExperience = await _appDbContext.Experiences.FindAsync(id);

        if (existExperience == null)
        {
            return NoContent();
        }

        _appDbContext.Experiences.Remove(existExperience);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }
}
