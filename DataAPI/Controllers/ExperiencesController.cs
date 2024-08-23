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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExperiences([FromRoute] int id)
    {
        var experience = await _appDbContext.Experiences.FindAsync(id);

        if (experience == null)
            return NotFound("Experience not found");

        return Ok(experience);
    }

    
    [HttpPost("createExperinces")]
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

    
    [HttpPut("updateExperince")]
    public async Task<IActionResult> UpdateExperiences([FromBody] UpdateExperienceDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existExperiences = await _appDbContext.Experiences.FindAsync(updateDto.Id);

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


    [HttpDelete("deleteExperience/{id}")]
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
