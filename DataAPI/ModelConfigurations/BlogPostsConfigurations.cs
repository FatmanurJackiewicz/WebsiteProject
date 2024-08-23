using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAPI.ModelConfigurations
{
    public class BlogPostsConfigurations : IEntityTypeConfiguration<BlogPosts>
    {
        public void Configure(EntityTypeBuilder<BlogPosts> builder)
        {
            builder.HasKey(bp => bp.Id);

            builder.Property(bp => bp.Title)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnType("varchar(100)");

            builder.Property(bp => bp.Content)
                   .IsRequired()
                   .HasColumnType("text");

            builder.Property(bp => bp.PublishDate)
                   .IsRequired()
                   .HasColumnType("datetime");


            //// Foreign key relationship
            //builder.HasOne(bp => bp.AuthorId)
            //       .IsRequired()
            //       .WithMany("blogpost")
            //       .HasColumnName("AuthorId")
            //       .HasColumnType("int")
            //       .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
