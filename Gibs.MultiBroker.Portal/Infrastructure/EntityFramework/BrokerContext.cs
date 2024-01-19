using Microsoft.EntityFrameworkCore;
using Gibs.Domain.Entities;


namespace Gibs.Infrastructure.EntityFramework
{
    public class BrokerContext(DbContextOptions<BrokerContext> options) : DbContext(options)
    {
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Insured> Insureds { get; set; }
        public DbSet<Insurer> Insurers { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<Underwriter> Underwriters { get; set; }
        //public DbSet<Policy> Policies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrokerContext).Assembly);

            modelBuilder.Entity<Broker>().HasData(
                new Broker("Omomowo Reliance", "12345678", "Omomowo", "Reliance", "omo@gmail.com", "08095482981", "123456")
            );

            //modelBuilder.Entity<Product>().HasData(
            //    new Product("1001", "V", null, "Third Party Motor", "Third Party"),
            //    new Product("1002", "V", null, "Comprehensive Motor", "Comprehensive"),
            //    new Product("2002", "A", null, "Home Insurance", "Home")
            //);

            modelBuilder.Entity<Insurer>().HasData(
                new Insurer("CORNERSTONE", "Cornerstone Insurance", ApiAuthEnum.JSON_WEB_TOKEN, "naicomId")
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
