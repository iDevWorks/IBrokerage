using Gibs.Domain.Entities;
using Gibs.Portal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTutor.EntityFramework.Configuration
{
    class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders")
                   .HasKey(x => x.OrderId);

            builder.Property(x => x.OrderId).HasColumnName("Id");
            builder.Property(x => x.CreatedUtc).HasColumnName("DateOrdered");
            builder.Property(x => x.TotalAmount).HasColumnName("TotalAmount").HasColumnType("decimal(18,2)");
            builder.Property(x => x.PaymentMethod).HasColumnName("PaymentMethod");
            builder.Property(x => x.PaymentStatus).HasColumnName("PaymentStatus").HasConversion<string>();
            builder.Property(x => x.TransReference).HasColumnName("TransReference");
            builder.Property(x => x.Remark).HasColumnName("Remark");
            builder.Property(x => x.PaymentDate).HasColumnName("PaymentDate");
        }
    }
}