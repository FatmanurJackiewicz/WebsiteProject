using DataAPI.Data;
using DataAPI.DTOs.AboutMe;
using DataAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AboutMeController : ControllerBase
{
    private static AppDbContext _appDbContext;

    public AboutMeController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAboutMe([FromRoute] int id)
    {
        var user = _appDbContext.AboutMe.SingleOrDefault(u => u.Id == id);

        if (user is null)
            return NotFound("User not found");

        return Ok(user);
    }

    [HttpPost("createAboutMe")]
    public async Task<IActionResult> PostAboutMe([FromBody] CreateAboutMeDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newAboutMe = new AboutMe
        {
            Introduction = createDto.Introduction,
            ImageUrl1 = createDto.ImageUrl1,
            ImageUrl2 = createDto.ImageUrl2
        };

        _appDbContext.AboutMe.Add(newAboutMe);
        await _appDbContext.SaveChangesAsync();

        return Ok(newAboutMe);
    }

    [HttpPut("updateAboutMe")]
    public async Task<IActionResult> UpdateAboutMe([FromBody] UpdateAboutMeDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existAboutMe = await _appDbContext.AboutMe.FindAsync(createDto.Id);

        if (existAboutMe is null)
            return NotFound("aboutme bulunamadi");

        existAboutMe.Introduction = createDto.Introduction;
        existAboutMe.ImageUrl1 = createDto.ImageUrl1;
        existAboutMe.ImageUrl2 = createDto.ImageUrl2;

        _appDbContext.AboutMe.Update(existAboutMe);
        await _appDbContext.SaveChangesAsync();

        return Ok(existAboutMe);
    }

    [HttpDelete("deleteAboutMe/{id}")]
    public async Task<IActionResult> DeleteAboutMe([FromRoute] int id)
    {
        var existAboutMe = await _appDbContext.AboutMe.FindAsync(id);

        if (existAboutMe == null)
        {
            return NoContent();
        }

        _appDbContext.AboutMe.Remove(existAboutMe);
        await _appDbContext.SaveChangesAsync(); 
        
        return Ok();
    }
}
