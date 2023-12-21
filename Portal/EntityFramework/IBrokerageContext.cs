﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.Entities;

namespace Portal.EntityFramework
{
    public class IBrokerageContext : IdentityDbContext<Broker>
    {
        public IBrokerageContext(DbContextOptions<IBrokerageContext> options) : base(options) 
        { }


        public DbSet<Broker> Brokers { get; set; }

        public DbSet<Claim> Claims { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Coverage> Coverages { get; set; }

        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Policy> Policies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}