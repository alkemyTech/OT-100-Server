using Microsoft.EntityFrameworkCore;
using OngProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OngProject.DataAccess.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(60);
            builder.Property(m => m.Phone)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(m => m.Message)
                .HasMaxLength(1500);
            builder.Property(m => m.FacebookUrl)
               .HasMaxLength(120);
            builder.Property(m => m.InstagramUrl)
                .HasMaxLength(120);
            builder.Property(m => m.LinkedInUrl)
                .HasMaxLength(120);
            builder.Property(m => m.AboutUsText)
                .HasMaxLength(1200);
            builder.Property(m => m.CreatedAt)
                .HasColumnType("SMALLDATETIME");
            builder.Property(m => m.UpdatedAt)
                .HasColumnType("SMALLDATETIME");
            builder.Property(m => m.DeletedAt)
                .HasColumnType("SMALLDATETIME");
        }
    }
}
