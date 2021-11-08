using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Configurations
{
    public class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
    {
        public void Configure(EntityTypeBuilder<UserDetails> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.FirstName)
                .HasMaxLength(60);
            builder.Property(m => m.LastName)
                .HasMaxLength(60);
            builder.Property(m => m.Photo)
                .HasMaxLength(240);
            builder.Property(m => m.CreatedAt)
              .HasColumnType("SMALLDATETIME");
            builder.Property(m => m.UpdatedAt)
                .HasColumnType("SMALLDATETIME");
            builder.Property(m => m.DeletedAt)
                .HasColumnType("SMALLDATETIME");
        }

    }
}
