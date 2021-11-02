﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Domain.Entities;

namespace OngProject.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.FirstName)
                .IsRequired()
                .HasMaxLength(60);
            builder.Property(m => m.LastName)
                .IsRequired()
                .HasMaxLength(60);
            builder.HasIndex(m => m.Email)
                .IsUnique();
            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(m => m.Password)
                .IsRequired();
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
