using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAPI.ModelConfigurations;

public class ProjectsConfigurations : IEntityTypeConfiguration<Projects>
{
    public void Configure(EntityTypeBuilder<Projects> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnType("text")
            .IsRequired(false);


        builder.Property(a => a.ImageUrl)
            .HasMaxLength(255)
            .HasColumnType("varchar(255)");


    }
}
