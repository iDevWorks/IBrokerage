using Gibs.Domain.Entities;
using Gibs.Portal.Domain.Entities;
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
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.Address).HasColumnName("Address");
            builder.Property(x => x.Phone).HasColumnName("Phone");
            builder.Property(x => x.Email).HasColumnName("Email");
        }
    }
}