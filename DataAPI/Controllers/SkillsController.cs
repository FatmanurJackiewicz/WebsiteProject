using DataAPI.Data;
using DataAPI.DTOs.Skills;
using DataAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SkillsController : ControllerBase
{
	private static AppDbContext _appDbContext;

	public SkillsController(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;

	}


	[HttpGet()]
	public async Task<IActionResult> GetSkills()
	{
		var SkillsCount = _appDbContext.Skills.ToList().Count();

		if (SkillsCount == 0)
			return NotFound("Skills not found");

		var skills = _appDbContext.Skills.FirstOrDefault();

		var skillsDto = new SkillsDetailsDto
		{
			Description = skills.Description
		};

		return Ok(skillsDto);
	}

	[HttpPost("createSkills")]
	public async Task<IActionResult> PostSkills([FromBody] CreateSkillsDto createDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var newSkills = new Skills
		{
			Description = createDto.Description
		};
		_appDbContext.Skills.Add(newSkills);
		await _appDbContext.SaveChangesAsync();

		return Ok();
	}

	[HttpPost("updateSkills")]
	public async Task<IActionResult> UpdateSkills([FromBody] UpdateSkillsDto updateDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var existSkills = _appDbContext.Skills.FirstOrDefault();

		if (existSkills is null)
			return NotFound("Skill is not found");

		existSkills.Description = updateDto.Description;

		_appDbContext.Skills.Update(existSkills);
		await _appDbContext.SaveChangesAsync();

		return Ok(existSkills);
	}







}
