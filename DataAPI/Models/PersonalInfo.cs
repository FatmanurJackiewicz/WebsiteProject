﻿using AuthAPI.Models;

namespace DataAPI.Models;

public class PersonalInfo
{
    public int Id { get; set; }
    public string About { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
 
}

