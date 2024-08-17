using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Username { get; set; }
    public string ConfirmPassword { get; internal set; }
    public EmailAddressAttribute Email { get; internal set; }

    [Required, MaxLength(255)]
    public string Password;


    public IdentityRole Role;

}
