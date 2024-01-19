using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTutor.EntityFramework.Configuration
{
    class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders")
                   .HasKey(x => x.Id);

            builder.HasMany(x => x.Policies)
                   .WithOne().IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Id).HasColumnName("OrderId");
            builder.Property(x => x.CreatedUtc).HasColumnName("CreatedUtc");
            builder.Property(x => x.TotalAmount).HasColumnName("TotalAmount");
            builder.Property(x => x.PaymentMethod).HasColumnName("PaymentMethod");
            builder.Property(x => x.PaymentStatus).HasColumnName("PaymentStatus").HasConversion<string>();
            builder.Property(x => x.Reference).HasColumnName("Reference");
            builder.Property(x => x.Remarks).HasColumnName("Remark");
            builder.Property(x => x.PaymentUtc).HasColumnName("PaymentUtc");
        }
    }
}