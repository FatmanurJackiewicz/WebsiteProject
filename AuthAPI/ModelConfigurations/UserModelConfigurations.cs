using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthAPI.ModelConfigurations;

public class UserModelConfigurations : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
               .IsRequired();

        builder.Property(u => u.Username)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(u => u.PasswordHash)
               .IsRequired()
               .HasMaxLength(255);
       

        //builder.HasMany(u => u.RefreshTokens)
        //    .WithOne()
        //    .HasForeignKey(r => r.UserId)
        //    .IsRequired()
        //    .OnDelete(DeleteBehavior.NoAction);
        
        //role'de ne olacak?
        builder.HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(r => r.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
