using Microsoft.EntityFrameworkCore;
using Gibs.Domain.Entities;
using Gibs.Portal.Domain.Entities;

namespace Gibs.Infrastructure.EntityFramework
{
    public class IBrokerageContext(DbContextOptions<IBrokerageContext> options) : DbContext(options)
    {
        public DbSet<Broker> Brokers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }

        //public DbSet<Payment> Payments { get; set; }

        public DbSet<Policy> Policies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
