using DataAPI.Data;
using DataAPI.DTOs.AboutMe;
using DataAPI.Models;
using Microsoft.AspNetCore.Components.Forms;
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

	[HttpGet()]
	public async Task<IActionResult> GetAboutMe()
	{
		var aboutMeCount = _appDbContext.AboutMe.ToList().Count();

		if (aboutMeCount == 0)
			return NotFound("User not found");

		var aboutMe = _appDbContext.AboutMe.FirstOrDefault();

		var aboutMeDto = new DetailsAboutMeDto
		{
			Introduction = aboutMe.Introduction,
			ImageUrl1 = aboutMe.ImageUrl1,
			ImageUrl2 = aboutMe.ImageUrl2
		};

		return Ok(aboutMeDto);
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

		return Ok();
	}

	[HttpPost("updateAboutMe")]
	public async Task<IActionResult> UpdateAboutMe([FromBody] UpdateAboutMeDto updateDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var existAboutMe = _appDbContext.AboutMe.FirstOrDefault();

		if (existAboutMe is null)
			return NotFound("AboutMe is not found");

		existAboutMe.Introduction = updateDto.Introduction;
		existAboutMe.ImageUrl1 = updateDto.ImageUrl1;
		existAboutMe.ImageUrl2 = updateDto.ImageUrl2;

		_appDbContext.AboutMe.Update(existAboutMe);
		await _appDbContext.SaveChangesAsync();

		return Ok(existAboutMe);
	}
	
}
