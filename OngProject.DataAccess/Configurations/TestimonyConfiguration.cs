using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Configurations
{
    public class TestimonyyConfiguration : IEntityTypeConfiguration<Testimony>
    {
        public void Configure(EntityTypeBuilder<Testimony> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(m => m.Image)
                .HasMaxLength(240);

            builder.Property(m => m.Content)
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
