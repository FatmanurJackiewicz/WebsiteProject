﻿using System.ComponentModel.DataAnnotations;

namespace DataAPI.DTOs.Skills
{
	public class SkillsDetailsDto
	{
		[Required]
		public List<string> SkillsList { get; set; }
	}
}
