namespace Portal.Entities
{
    public class Policy
    {
        public Policy() { }

        public Policy(InsuranceCompany insuranceCompany, string name, DateTime endDate, decimal premiumAmount) 
        {
            ArgumentNullException.ThrowIfNull(insuranceCompany);

            if(string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentNullException(nameof(name));

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(premiumAmount);

            Name = name;
            IssuingCompany = insuranceCompany;
            IssuingCompanyId = insuranceCompany.Id;
            CreatedDate = DateTime.UtcNow;
            EndDate = endDate;
            PremiumAmount = premiumAmount;
        }

        public string Id { get; private set; }
        public string IssuingCompanyId {  get; private set; } 
        public string ClientId { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal PremiumAmount { get; private set; }

        public virtual InsuranceCompany IssuingCompany { get; private set; }
        public virtual Client Client { get; private set; }
        public virtual ICollection<Claim> Claims { get; private set; }
        public virtual ICollection<Coverage> Coverages { get; private set; }
        public virtual ICollection<Payment> Payments { get; private set; }
    }
}