using Microsoft.EntityFrameworkCore;
using Gibs.Domain.Entities;


namespace Gibs.Infrastructure.EntityFramework
{
    public class BrokerContext(DbContextOptions<BrokerContext> options) : DbContext(options)
    {
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Insured> Insureds { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Underwriter> Underwriters { get; set; }
        public DbSet<Policy> Policies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrokerContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
