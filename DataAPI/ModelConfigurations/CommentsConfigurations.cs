﻿using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAPI.ModelConfigurations
{
    public class CommentsConfigurations : IEntityTypeConfiguration<Comments>
    {
        public void Configure(EntityTypeBuilder<Comments> builder)
        {
            builder.HasKey(c => c.Id);


            builder.Property(c => c.Content)
                   .IsRequired()
                   .HasColumnType("text");

            builder.Property(c => c.CreatedDate)
                   .IsRequired()
                   .HasColumnType("datetime");

            builder.Property(c => c.IsApproved)
                   .IsRequired()
                   .HasColumnType("bit");

            builder.Property(c => c.BlogPostId)
                   .IsRequired()
                   .HasColumnType("int");

            builder.Property(c => c.UserId)
                   .IsRequired()
                   .HasColumnType("int");

            //// Foreign key relationships
            //builder.HasOne(c => c.BlogPost)
            //       .WithMany()  // Assuming BlogPosts can have multiple Comments
            //       .HasForeignKey(c => c.BlogPostId)
            //       .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(c => c.User)
            //       .WithMany()  // Assuming User can have multiple Comments
            //       .HasForeignKey(c => c.UserId)
            //       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
