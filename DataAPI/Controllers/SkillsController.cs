using DataAPI.Data;
using DataAPI.DTOs.Skills;
using DataAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SkillsController : ControllerBase
{
	private readonly AppDbContext _appDbContext;

	public SkillsController(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;

	}

	[HttpGet()]
	public async Task<IActionResult> GetSkills()
	{
		var skillsList = _appDbContext.Skills.ToList();

		if (skillsList is null)
			return NotFound("Skills not found");

		var skillsDto = new SkillsDetailsDto
		{
			SkillsList = skillsList.Select(s => s.Name).ToList()
		};

		return Ok(skillsDto);
	}

	[HttpPost("createSkills")]
	public async Task<IActionResult> CreateSkills([FromBody] CreateSkillsDto createDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		foreach (var skillName in createDto.SkillsList)
		{
			var newSkill = new Skills
			{
				Name = skillName
			};
			_appDbContext.Skills.Add(newSkill);
		}
		
		await _appDbContext.SaveChangesAsync();

		return Ok();
	}

	[HttpPost("updateSkills")]
	public async Task<IActionResult> UpdateSkills([FromBody] UpdateSkillsDto updateDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var existSkills = _appDbContext.Skills.ToList();

		if (existSkills is null)
			return NotFound("Skill is not found");

		var index = 0;
		foreach (var skillName in updateDto.SkillsList)
		{			
			if (index == existSkills.Count)
				break;
			existSkills[index].Name = skillName;
			index++;			
		}

		await _appDbContext.SaveChangesAsync();

		return Ok(existSkills);
	}

    [HttpPost("deleteSkill")]
    public async Task<IActionResult> DeleteSkill([FromBody] DeleteSkillDto deleteSkillDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

		var existSkill = _appDbContext.Skills.Where(s => s.Name == deleteSkillDto.SkillName).FirstOrDefault();

        if (existSkill is null)
            return NoContent();

		_appDbContext.Remove(existSkill);

        await _appDbContext.SaveChangesAsync();

        return Ok();
    }







}
