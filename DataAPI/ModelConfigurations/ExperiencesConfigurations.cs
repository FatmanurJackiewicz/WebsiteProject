using DataAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.ModelConfigurations
{
    public class ExperiencesConfigurations : IEntityTypeConfiguration<Experiences>
    {
        public void Configure(EntityTypeBuilder<Experiences> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(e => e.Company)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(e => e.StartDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(e => e.EndDate)
                .HasColumnType("date")
                .IsRequired(false);

            builder.Property(e => e.Description)
                .HasColumnType("text")
                .IsRequired(false);
        }
    }

}
