using DataAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.ModelConfigurations
{
    public class EducationsConfigurations : IEntityTypeConfiguration<Educations>
    {
        public void Configure(EntityTypeBuilder<Educations> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.Degree)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnType("varchar(50)");

            builder.Property(e => e.School)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnType("varchar(100)");

            builder.Property(e => e.StartDate)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(e => e.EndDate)
                   .HasColumnType("date");

        }
    }

}
