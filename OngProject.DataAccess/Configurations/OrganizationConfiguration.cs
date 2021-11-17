using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(60);
            builder.Property(m => m.Image);
            builder.Property(m => m.Address)
                .HasMaxLength(1200);
            builder.Property(m => m.Phone)
                .HasMaxLength(10);
            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(m => m.WelcomeText)
                .IsRequired()
                .HasMaxLength(200);
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
