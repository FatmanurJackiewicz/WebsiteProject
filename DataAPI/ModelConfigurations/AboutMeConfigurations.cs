using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAPI.ModelConfigurations;

public class AboutMeConfigurations : IEntityTypeConfiguration<AboutMe>
{
    public void Configure(EntityTypeBuilder<AboutMe> builder)
    {
        //primary key
        builder.HasKey(a => a.Id);

        // Properties
        builder.Property(a => a.Introduction)
               .IsRequired()
               .HasColumnType("text");

        builder.Property(a => a.ImageUrl1)
               .HasMaxLength(255)
               .HasColumnType("varchar(255)");

        builder.Property(a => a.ImageUrl2)
               .HasMaxLength(255)
               .HasColumnType("varchar(255)");
    }
}
