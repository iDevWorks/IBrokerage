using Gibs.Portal.Domain.Entities;

namespace Gibs.Domain.Entities
{
    public class Policy
    {
        public Policy() { }

        public Policy(Underwriter insuranceCompany, string name, DateTime endDate, decimal premiumAmount) 
        {
            ArgumentNullException.ThrowIfNull(insuranceCompany);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(premiumAmount);

            Name = name;
            Underwriter = insuranceCompany;
            CreatedDate = DateTimeOffset.UtcNow;
            EndDate = endDate;
            PremiumAmount = premiumAmount;
        }

        public string Id { get; private set; }
        public string ClientId { get; private set; }
        public string Name { get; private set; }
        public DateTimeOffset CreatedDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public decimal PremiumAmount { get; private set; }

        public virtual Underwriter Underwriter { get; private set; }
        public virtual Client Client { get; private set; }
        //public virtual ICollection<Claim> Claims { get; private set; }
        //public virtual ICollection<Coverage> Coverages { get; private set; }
        //public virtual ICollection<Payment> Payments { get; private set; }
    }
}