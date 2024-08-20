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
    public string Email { get; internal set; }

    [Required, MaxLength(255)]
    public byte[] PasswordHash { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

}

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = default;

    public virtual ICollection<UserRole> UserRoles { get; set; } = default;

}

public class UserRole
{
    public int UserId { get; set; }
    public User User { get; set; } = default;

    public int RoleId { get; set; }
    public Role Role { get; set; } = default;
}
