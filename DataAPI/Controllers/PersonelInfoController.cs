using DataAPI.Data;
using DataAPI.DTOs.PersonalInfo;
using DataAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonelInfoController : ControllerBase
{
    private static AppDbContext _appDbContext;

    public PersonelInfoController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPersonalInfo([FromRoute] int id)
    {
        var personalInfo = await _appDbContext.PersonalInfo.FindAsync(id);

        if (personalInfo == null)
        {
            return NotFound();
        }

        return Ok(personalInfo);
    }

    [HttpPost("createPersonalInfo")]
    public async Task<IActionResult> PostPersonalInfo(CreatePersonalInfo createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newPersonalInfo = new PersonalInfo
        {
            About = createDto.About,
            Name = createDto.Name,
            Surname = createDto.Surname,
            BirthDate = createDto.BirthDate
        };

        _appDbContext.PersonalInfo.Add(newPersonalInfo);
        await _appDbContext.SaveChangesAsync();

        return Ok(newPersonalInfo);
    }

    [HttpPut("updatePersonalInfo/{id}")]
    public async Task<IActionResult> PutPersonalInfo(UpdatePersonalInfo updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);


        var existPersonalInfo = await _appDbContext.PersonalInfo.FindAsync(updateDto.Id);

        if (existPersonalInfo is null)
            return NotFound("PersonalInfo bulunamadi");

        existPersonalInfo.About = updateDto.About;
        existPersonalInfo.Name = updateDto.Name;
        existPersonalInfo.Surname = updateDto.Surname;
        existPersonalInfo.BirthDate = updateDto.BirthDate;

        _appDbContext.Update(existPersonalInfo);
        await _appDbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("deletePersonalInfo/{id}")]
    public async Task<IActionResult> DeletePersonalInfo([FromRoute] int id)
    {
        var personalInfo = await _appDbContext.PersonalInfo.FindAsync(id);
        if (personalInfo == null)
        {
            return NoContent();
        }

        _appDbContext.PersonalInfo.Remove(personalInfo);
        await _appDbContext.SaveChangesAsync();

        return Ok();
    }
}
