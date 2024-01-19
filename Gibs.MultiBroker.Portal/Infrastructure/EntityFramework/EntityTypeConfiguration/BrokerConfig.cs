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

            builder.HasMany(x => x.Insureds).WithOne().IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Orders).WithOne().IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Policies).WithOne().IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Products).WithOne().IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Underwriters).WithOne().IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);


            builder.Property(x => x.Id).HasColumnName("BrokerId");
            builder.Property(x => x.CreatedUtc).HasColumnName("CreatedUtc");
            builder.Property(x => x.Title).HasColumnName("Title");
            builder.Property(x => x.FirstName).HasColumnName("FirstName");
            builder.Property(x => x.LastName).HasColumnName("LastName");
            builder.Property(x => x.Phone).HasColumnName("Phone");
            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.Password).HasColumnName("Password");
            builder.Property(x => x.IsActive).HasColumnName("IsActive");

            builder.Property(x => x.BrokerName).HasColumnName("BrokerName");
            builder.Property(x => x.RegistrationNo).HasColumnName("RegistrationNo");

            builder.OwnsOne(x => x.Kyc, nb =>
            {
                nb.Property(x => x.KycNumber).HasColumnName("KycNumber");
                nb.Property(x => x.IssueDate).HasColumnName("IssueDate");
                nb.Property(x => x.ExpiryDate).HasColumnName("ExpiryDate");
                nb.Property(x => x.KycType).HasColumnName("KycType").HasConversion<string>();
                nb.Property(x => x.TaxNumber).HasColumnName("TaxNumber");
            });
        }
    }
}