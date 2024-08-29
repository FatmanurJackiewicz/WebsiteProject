﻿using System.ComponentModel.DataAnnotations;

namespace AdminPanelMVC.Models.Skills
{
	public class CreateSkillsViewModel
	{
		[Required]
		public string Description { get; set; }
		public List<string> SkillsList { get; set; }
	}
}
