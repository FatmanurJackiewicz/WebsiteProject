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
public class PersonalInfoController : ControllerBase
{
    private static AppDbContext _appDbContext;

    public PersonalInfoController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("personal-info")]
    public async Task<IActionResult> GetPersonalInfo()
    {
        var personalInfo = _appDbContext.PersonalInfo.FirstOrDefault();

        if (personalInfo == null)
        {
            return NotFound();
        }

        var personalInfoDto = new CreatePersonalInfoDto
        {
            About = personalInfo.About,
            Name = personalInfo.Name,
            Surname = personalInfo.Surname,
            BirthDate = personalInfo.BirthDate
        };

        return Ok(personalInfoDto);
    }

    [HttpPost("createPersonalInfo")]
    public async Task<IActionResult> PostPersonalInfo(CreatePersonalInfoDto createDto)
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

    [HttpPost("updatePersonalInfo")]
    public async Task<IActionResult> PutPersonalInfo(UpdatePersonalInfoDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);


        var existPersonalInfo = _appDbContext.PersonalInfo.FirstOrDefault();

        if (existPersonalInfo is null)
            return NotFound("PersonalInfo not found.");

        existPersonalInfo.About = updateDto.About;
        existPersonalInfo.Name = updateDto.Name;
        existPersonalInfo.Surname = updateDto.Surname;
        existPersonalInfo.BirthDate = updateDto.BirthDate;

        _appDbContext.Update(existPersonalInfo);
        await _appDbContext.SaveChangesAsync();

        return NoContent();
    }
    
}
