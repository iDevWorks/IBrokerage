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
        public DbSet<Underwriter> Underwriters { get; set; }
        public DbSet<Policy> Policies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrokerContext).Assembly);

            modelBuilder.Entity<Broker>()
                .HasOne(b => b.Kyc)  // Assuming 'Kyc' is the navigation property
                .WithOne();

            modelBuilder.Entity<Broker>().HasData(
                new Broker("Omomowo Reliance", "12345678", "Omomowo", "Reliance", "omomowosymeon45@gmail.com", "08095482981", "123456")
            );

            modelBuilder.Entity<Product>().HasData(
                new Product("1001", "classId", "", "third-party-insurance", "shortname"),
                new Product("1002", "classId1", "", "third-party-insurance-2", "shortname2")
            );

            modelBuilder.Entity<Insurer>().HasData(
                new Insurer("001", "cornerstone", ApiAuthEnum.QUERY_STRING, "naicomId")
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
