using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models;

public class UserModel
{
    [PrimaryKey]
    public int Id { get; set; }

    [MaxLength(50)]
    public string Username { get; set; }

    [MaxLength(255)]
    public required IPasswordHasher<PasswordHasher> PasswordHash;

    
    public IdentityRole Role { get; set; }


}
