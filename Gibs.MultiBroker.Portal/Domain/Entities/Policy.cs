namespace Gibs.Domain.Entities
{
    public class Policy
    {
        #pragma warning disable CS8618
        public Policy() { }
        #pragma warning restore CS8618

        public Policy(Product product, Insured insured, 
            string policyNo, DateOnly startDate, DateOnly endDate, decimal sumInsured, decimal grossPremium) 
        {
            ArgumentNullException.ThrowIfNull(product);
            ArgumentNullException.ThrowIfNull(insured);

            ArgumentException.ThrowIfNullOrWhiteSpace(policyNo);
            ArgumentNullException.ThrowIfNull(startDate);
            ArgumentNullException.ThrowIfNull(endDate);

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sumInsured);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(grossPremium);

            Product = product;
            Insured = insured;
            InsuredName = insured.FullName;

            PolicyNo = policyNo;
            CreatedUtc = DateTime.UtcNow;
            StartDate = startDate;
            EndDate = endDate;

            GrossPremium = grossPremium;
            SumInsured = sumInsured;
            Commission = 0;
        }

        public string PolicyNo { get; private set; }
        public DateTime CreatedUtc { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }

        public string InsuredName { get; private set; }
        public string Status { get; private set; } = "PENDING";//CANCELED APPROVED
        public DateTime? ApprovedUtc { get; private set; }
        public string? ApprovedBy { get; private set; }

        public string? AgentId { get; private set; } //marketer
        public string? ChannelId { get; private set; }
        public string? SubChannelId { get; private set; }

        public string FxCurrency { get; private set; } = "NGN";
        public decimal FxRate { get; private set; } = 1;

        public decimal SumInsured { get; private set; }
        public decimal GrossPremium { get; private set; }
        public decimal Commission { get; private set; }

        public Product Product { get; private set; }
        public Insured Insured { get; private set; }
        public Insurer? Insurer { get; private set; }

        //public Insurer? LeadInsurer { get; private set; }
    }
}