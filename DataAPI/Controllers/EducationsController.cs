using DataAPI.Data;
using DataAPI.DTOs.Educations;
using DataAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EducationsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public EducationsController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEducations([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var educations = await _appDbContext.Educations.FindAsync(id);

        if (educations is null)
            return NotFound("Educations not found");

        return Ok(educations);
    }

    [HttpPost("createEducation")]
    public async Task<IActionResult> CreateEducations([FromBody] CreateEducationsDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //business rules
        if (createDto.StartDate > createDto.EndDate)
            return BadRequest("Start date cannot be later than end date.");

        var newEducation = new Educations
        {
            Degree = createDto.Degree,
            School = createDto.School,
            StartDate = createDto.StartDate,
            EndDate = createDto.EndDate
        };

        _appDbContext.Educations.Add(newEducation);
        await _appDbContext.SaveChangesAsync();

        return Ok(newEducation);
    }

    [HttpPut("updateEducation")]
    public async Task<IActionResult> UpdateEducations([FromBody] UpdateEducationsDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existEducation = await _appDbContext.Educations.FindAsync(updateDto.Id);

        if (existEducation is null)
            return NotFound("Education not found");

        // StartDate ve EndDate arasındaki iş kuralı
        if (updateDto.StartDate > updateDto.EndDate)
            return BadRequest("Start date cannot be later than end date.");

        existEducation.Degree = updateDto.Degree;
        existEducation.School = updateDto.School;
        existEducation.StartDate = updateDto.StartDate;
        existEducation.EndDate = updateDto.EndDate;

        _appDbContext.Educations.Update(existEducation);
        await _appDbContext.SaveChangesAsync();

        return Ok(existEducation);
    }


    [HttpDelete("deleteEducation/{id}")]
    public async Task<IActionResult> DeleteEducation([FromRoute] int id)
    {
        var existEducation = await _appDbContext.Educations.FindAsync(id);

        if (existEducation == null)
        {
            return NoContent();
        }

        _appDbContext.Educations.Remove(existEducation);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }
}

