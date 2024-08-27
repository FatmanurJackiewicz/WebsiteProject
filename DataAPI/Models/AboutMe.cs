﻿using AuthAPI.Models;

namespace DataAPI.Models;

public class AboutMe
{
    public int Id { get; set; }
    public string Introduction { get; set; } = "Intro";
    public string ImageUrl1 { get; set; }
    public string ImageUrl2 { get; set; }
}
