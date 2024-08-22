using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthAPI.ModelConfigurations;

public class RoleModelConfigurations : IEntityTypeConfiguration<RoleModel>
{
    public void Configure(EntityTypeBuilder<RoleModel> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
               .IsRequired();

        builder.Property(u => u.Name)
               .IsRequired()
               .HasMaxLength(50);

        //user ile olan ilişkisini burada da belirtmeye gerek var mı?
        builder.HasData(
            new RoleModel { Id = 1, Name = "admin" },
            new RoleModel { Id = 2, Name = "commenter" });
            
    }
}
