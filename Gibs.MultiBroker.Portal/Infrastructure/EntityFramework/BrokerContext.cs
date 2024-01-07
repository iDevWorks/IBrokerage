using Microsoft.EntityFrameworkCore;
using Gibs.Domain.Entities;
using Gibs.Portal.Domain.Entities;

namespace Gibs.Infrastructure.EntityFramework
{
    public class BrokerContext(DbContextOptions<BrokerContext> options) : DbContext(options)
    {
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Underwriter> Underwriters { get; set; }
        public DbSet<Policy> Policies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Broker>(e =>
            {
                e.ToTable("Brokers").HasKey(d => d.Id);
            });
            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Orders").HasKey(d => d.OrderId);
            });
            modelBuilder.Entity<Client>(e =>
            {
                e.ToTable("Clients").HasKey(d => d.Id);
            });
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Products").HasKey(d => d.Id);
            });
            modelBuilder.Entity<Underwriter>(e =>
            {
                e.ToTable("Underwriters").HasKey(d => d.Id);
            });
            modelBuilder.Entity<Policy>(e =>
            {
                e.ToTable("Policies").HasKey(d => d.PolicyNo);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
