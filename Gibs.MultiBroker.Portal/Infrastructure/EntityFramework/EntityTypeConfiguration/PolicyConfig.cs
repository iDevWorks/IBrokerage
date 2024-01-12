using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTutor.EntityFramework.Configuration
{
    class PolicyConfig : IEntityTypeConfiguration<Policy>
    {
        public void Configure(EntityTypeBuilder<Policy> builder)
        {
            builder.ToTable("Policies")
                   .HasKey(x => x.PolicyNo);

            builder.Property(x => x.PolicyNo).HasColumnName("PolicyNo");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.CreatedDate).HasColumnName("CreatedDate");
            builder.Property(x => x.StartDate).HasColumnName("StartDate");
            builder.Property(x => x.EndDate).HasColumnName("EndDate");
            builder.Property(x => x.GrossPremium).HasColumnName("GrossPremium").HasColumnType("decimal(18,2)");
            builder.Property(x => x.Status).HasColumnName("Status");
            builder.Property(x => x.SumInsured).HasColumnName("SumInsured").HasColumnType("decimal(18,2)");
            builder.Property(x => x.Commision).HasColumnName("Commision").HasColumnType("decimal(18,2)");
        }
    }
}