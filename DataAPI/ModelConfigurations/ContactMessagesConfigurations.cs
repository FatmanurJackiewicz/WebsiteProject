using DataAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.ModelConfigurations
{
    public class ContactMessagesConfigurations : IEntityTypeConfiguration<ContactMessages>
    {
        public void Configure(EntityTypeBuilder<ContactMessages> builder)
        {
            // Primary Key
            builder.HasKey(cm => cm.Id);

            // Properties
            builder.Property(cm => cm.Name)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnType("varchar(50)");

            builder.Property(cm => cm.Email)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnType("varchar(100)");

            builder.Property(cm => cm.Subject)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnType("varchar(100)");

            builder.Property(cm => cm.Message)
                   .IsRequired()
                   .HasColumnType("text");

            builder.Property(cm => cm.SentDate)
                   .IsRequired()
                   .HasColumnType("datetime");

            builder.Property(cm => cm.IsRead)
                   .IsRequired()
                   .HasColumnType("bit");

            builder.Property(cm => cm.Reply)
                   .HasColumnType("text");

            builder.Property(cm => cm.ReplyDate)
                   .HasColumnType("datetime");
        }
    }

}
