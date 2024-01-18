﻿using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTutor.EntityFramework.Configuration
{
    class UnderwriterConfig : IEntityTypeConfiguration<Underwriter>
    {
        public void Configure(EntityTypeBuilder<Underwriter> builder)
        {
            builder.ToTable("Underwriters")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.MappedFields).HasColumnName("MappedFields");
            builder.Property(x => x.ApiKey1Username).HasColumnName("ApiKey1Username");
            builder.Property(x => x.ApiKey2Password).HasColumnName("ApiKey2Password");
        }
    }
}