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

            builder.HasOne(e => e.User)
                .WithMany()
                .IsRequired()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
