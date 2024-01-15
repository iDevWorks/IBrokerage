using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTutor.EntityFramework.Configuration
{
    class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products")
                   .HasKey(x => x.ProductId);

            builder.Property(x => x.ProductId).HasColumnName("Id");
            builder.Property(x => x.ClassId).HasColumnName("ClassId");
            builder.Property(x => x.MidClassId).HasColumnName("MidClassId");
            builder.Property(x => x.ProductName).HasColumnName("Name");
            builder.Property(x => x.ShortName).HasColumnName("ShortName");
            builder.Property(x => x.NaiconTypeId).HasColumnName("NaiconTypeId");
        }
    }
}