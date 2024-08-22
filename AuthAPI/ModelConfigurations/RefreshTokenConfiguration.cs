using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthAPI.ModelConfigurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {

        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.Token)
               .IsRequired();

        builder.Property(rt => rt.ExpiryDate)
               .IsRequired();

        builder.Property(rt => rt.IsUsed)
               .IsRequired();

        builder.Property(rt => rt.IsRevoked)
               .IsRequired();

        builder.HasOne(rt => rt.User)
               .WithMany()
               .HasForeignKey(rt => rt.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
    }
}