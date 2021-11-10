using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Configurations
{
    public class SlideConfiguration: IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.ImageUrl);
            builder.Property(m => m.Text)
                .IsRequired()
                .HasMaxLength(8000);
            builder.Property(m => m.Order)
                .IsRequired();
            builder.Property(m => m.CreatedAt)
                .HasColumnType("SMALLDATETIME");
            builder.Property(m => m.UpdatedAt)
                .HasColumnType("SMALLDATETIME");
            builder.Property(m => m.DeletedAt)
                .HasColumnType("SMALLDATETIME");
        }
    }
}