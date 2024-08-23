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

            // Columns
            builder.Property(bp => bp.Id)
                   .IsRequired()
                   .HasColumnName("Id")
                   .HasColumnType("int");

            builder.Property(bp => bp.Title)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("Title")
                   .HasColumnType("varchar(100)");

            builder.Property(bp => bp.Content)
                   .IsRequired()
                   .HasColumnName("Content")
                   .HasColumnType("text");

            builder.Property(bp => bp.PublishDate)
                   .IsRequired()
                   .HasColumnName("PublishDate")
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
