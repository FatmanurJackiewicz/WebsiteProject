using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthAPI.ModelConfigurations;

public class RoleModelConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
               .IsRequired();

        builder.Property(u => u.Name)
               .IsRequired()
               .HasMaxLength(50);

        //user ile olan ilişkisini burada da belirtmeye gerek var mı?
        builder.HasData(
            new Role { Id = 1, Name = "admin" },
            new Role { Id = 2, Name = "commenter" });
            
    }
}
