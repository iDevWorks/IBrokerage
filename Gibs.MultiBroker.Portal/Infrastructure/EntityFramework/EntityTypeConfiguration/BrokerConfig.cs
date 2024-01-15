using Gibs.Domain.Entities;
using iDevWorks.Paystack;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTutor.EntityFramework.Configuration
{
    class BrokerConfig : IEntityTypeConfiguration<Broker>
    {
        public void Configure(EntityTypeBuilder<Broker> builder)
        {
            builder.ToTable("Brokers")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("BrokerId");
            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.Phone).HasColumnName("Phone");
            builder.Property(x => x.BrokerName).HasColumnName("Name");
            builder.Property(x => x.Address).HasColumnName("Address");
            builder.Property(x => x.Password).HasColumnName("Password");
            builder.Property(x => x.CreatedUtc).HasColumnName("CreatedUtc");
        }
    }
}