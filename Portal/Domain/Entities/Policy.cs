using Gibs.Portal.Domain.Entities;

namespace Gibs.Domain.Entities
{
    public class Policy
    {
        public Policy() { }

        public Policy(Product product, Client client, string name, DateTime endDate, decimal grossPremium, decimal sumInsured) 
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentNullException.ThrowIfNull(product);
            ArgumentNullException.ThrowIfNull(client);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(grossPremium);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sumInsured);

            Product = product;
            Client = client;
            Name = name;
            CreatedDate = DateTime.UtcNow;
            EndDate = endDate;
            GrossPremium = grossPremium;
            SumInsured = sumInsured;
        }

        public string PolicyNo { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal GrossPremium { get; private set; }
        public string Status { get; private set; }
        public decimal SumInsured { get; private set; }
        public decimal Commision { get; private set; }

        public virtual Underwriter? Underwriter { get; private set; }
        public virtual Product Product { get; private set; }

        public virtual Client Client { get; private set; }
    }
}