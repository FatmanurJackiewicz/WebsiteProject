using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthAPI.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; internal set; }
    public byte[] PasswordHash { get; set; }
    public string? ResetPasswordToken { get; set; }
    public int RoleId { get; set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
    public virtual Role Role { get; set; } = default!;

}


