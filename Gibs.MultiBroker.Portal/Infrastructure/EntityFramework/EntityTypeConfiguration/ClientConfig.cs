using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTutor.EntityFramework.Configuration
{
    class ClientConfig : IEntityTypeConfiguration<Insured>
    {
        public void Configure(EntityTypeBuilder<Insured> builder)
        {
            builder.ToTable("Customers")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("CustomerId");
            builder.Property(x => x.FirstName).HasColumnName("FirstName");
            builder.Property(x => x.LastName).HasColumnName("LastName");
            builder.Property(x => x.Address).HasColumnName("Address");
            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.Phone).HasColumnName("Phone");
            builder.Property(x => x.CreatedUtc).HasColumnName("CreatedUtc");
        }
    }
}