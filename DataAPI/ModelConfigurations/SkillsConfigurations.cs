using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAPI.ModelConfigurations
{
	public class SkillsConfigurations : IEntityTypeConfiguration<Skills>
	{
		public void Configure(EntityTypeBuilder<Skills> builder)
		{
			builder.Property(e => e.Description)
			.HasColumnType("text")
			.IsRequired(true);
		}
	}
}
