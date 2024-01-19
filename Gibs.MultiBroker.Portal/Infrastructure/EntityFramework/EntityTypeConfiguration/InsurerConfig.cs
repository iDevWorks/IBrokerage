using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTutor.EntityFramework.Configuration
{
    class InsurerConfig : IEntityTypeConfiguration<Insurer>
    {
        public void Configure(EntityTypeBuilder<Insurer> builder)
        {
            builder.ToTable("Insurers")
                   .HasKey(x => x.Id);

            builder.HasMany<Underwriter>()
                   .WithOne(x => x.Insurer).IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany<Policy>()
                   .WithOne(x => x.Insurer).IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Id).HasColumnName("InsurerId");
            builder.Property(x => x.InsurerName).HasColumnName("InsurerName");
            builder.Property(x => x.CreatedUtc).HasColumnName("CreatedUtc");
            builder.Property(x => x.ApiAuthStyle).HasColumnName("ApiAuthStyle");
            builder.Property(x => x.NaicomId).HasColumnName("NaicomId");
            builder.Property(x => x.IsActive).HasColumnName("IsActive");
        }
    }
}