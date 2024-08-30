using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAPI.ModelConfigurations
{
	public class SkillsConfigurations : IEntityTypeConfiguration<Skills>
	{
		public void Configure(EntityTypeBuilder<Skills> builder)
		{
			builder.HasKey(s => s.Id);
			builder.Property(s => s.Id).ValueGeneratedOnAdd();

			builder.Property(s => s.Name)
				.IsRequired();

		}
	}
}
