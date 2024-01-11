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
            builder.Property(x => x.DateOrdered).HasColumnName("DateOrdered");
            builder.Property(x => x.TotalAmount).HasColumnName("TotalAmount").HasColumnType("decimal(18,2)");
            builder.Property(x => x.PaymentMethod).HasColumnName("PaymentMethod");
            builder.Property(x => x.PaymentMethod).HasColumnName("PaymentMethod");
            builder.Property(x => x.TransReference).HasColumnName("TransReference");
            builder.Property(x => x.Client).HasColumnName("Client");
            builder.Property(x => x.Product).HasColumnName("Product");

        }
    }
}