﻿using AuthAPI.Models;

namespace DataAPI.Models;

public class Projects
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

    public virtual User User { get; set; }
}

