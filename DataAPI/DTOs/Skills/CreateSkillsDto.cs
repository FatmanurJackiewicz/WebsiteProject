﻿using System.ComponentModel.DataAnnotations;

namespace DataAPI.DTOs.Skills
{
	public class CreateSkillsDto
	{
        [Required]
        public List<string> SkillsList { get; set; }
    }
}
