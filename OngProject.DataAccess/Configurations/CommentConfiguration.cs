using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Configurations
{
    public class CommentConfiguration: IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(m => m.Body)
                .IsRequired()
                .HasMaxLength(8000);
            builder.Property(m => m.CreatedAt)
                .HasColumnType("SMALLDATETIME");
            builder.Property(m => m.UpdatedAt)
                .HasColumnType("SMALLDATETIME");
            builder.Property(m => m.DeletedAt)
                .HasColumnType("SMALLDATETIME");
        }
    }
}