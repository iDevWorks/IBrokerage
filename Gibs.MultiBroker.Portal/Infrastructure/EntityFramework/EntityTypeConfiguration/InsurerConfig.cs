using Gibs.Domain.Entities;
using iDevWorks.Paystack;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTutor.EntityFramework.Configuration
{
    class InsurerConfig : IEntityTypeConfiguration<Insurer>
    {
        public void Configure(EntityTypeBuilder<Insurer> builder)
        {
            builder.ToTable("Insurers")
                   .HasKey(x => x.InsurerId);

            builder.Property(x => x.InsurerId).HasColumnName("InsurerId");
            builder.Property(x => x.InsurerName).HasColumnName("InsurerName");
            builder.Property(x => x.CreatedUtc).HasColumnName("CreatedUtc");
            builder.Property(x => x.ApiAuthStyle).HasColumnName("ApiAuthStyle");
            builder.Property(x => x.NaicomId).HasColumnName("NaicomId");
            builder.Property(x => x.IsActive).HasColumnName("IsActive");
        }
    }
}