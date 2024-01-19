using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTutor.EntityFramework.Configuration
{
    class InsuredConfig : IEntityTypeConfiguration<Insured>
    {
        public void Configure(EntityTypeBuilder<Insured> builder)
        {
            builder.ToTable("Insureds")
                   .HasKey(x => x.Id);

            builder.HasMany<Order>()
                   .WithOne(x => x.Insured).IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany<Policy>()
                   .WithOne(x => x.Insured).IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Id).HasColumnName("InsuredId");
            builder.Property(x => x.CreatedUtc).HasColumnName("CreatedUtc");
            builder.Property(x => x.Title).HasColumnName("Title");
            builder.Property(x => x.FirstName).HasColumnName("FirstName");
            builder.Property(x => x.LastName).HasColumnName("LastName");
            builder.Property(x => x.Phone).HasColumnName("Phone");
            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.Password).HasColumnName("Password");
            builder.Property(x => x.IsActive).HasColumnName("IsActive");

            builder.Property(x => x.IsCorporate).HasColumnName("IsCorporate");
            builder.Property(x => x.CompanyName).HasColumnName("CompanyName");
            builder.Property(x => x.DateOfBirthOrReg).HasColumnName("DateOfBirthOrReg");

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