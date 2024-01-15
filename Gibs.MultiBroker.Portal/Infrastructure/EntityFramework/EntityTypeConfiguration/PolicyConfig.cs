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
            builder.Property(x => x.InsuredName).HasColumnName("Name");
            builder.Property(x => x.CreatedUtc).HasColumnName("CreatedDate");
            builder.Property(x => x.StartDate).HasColumnName("StartDate");
            builder.Property(x => x.EndDate).HasColumnName("EndDate");
            builder.Property(x => x.GrossPremium).HasColumnName("GrossPremium");
            builder.Property(x => x.Status).HasColumnName("Status");
            builder.Property(x => x.SumInsured).HasColumnName("SumInsured");
            builder.Property(x => x.Commission).HasColumnName("Commision");
        }
    }
}