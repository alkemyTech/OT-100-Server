using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(60);
            builder.Property(m => m.FacebookUrl)
                .HasMaxLength(120);
            builder.Property(m => m.InstagramUrl)
                .HasMaxLength(120);
            builder.Property(m => m.LinkedInUrl)
                .HasMaxLength(120);
            builder.Property(m => m.Image)
                .IsRequired()
                .HasMaxLength(240);
            builder.Property(m => m.Description)
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