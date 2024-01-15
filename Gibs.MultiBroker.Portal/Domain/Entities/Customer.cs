namespace Gibs.Domain.Entities
{
    public class Customer : Person
    {
        #pragma warning disable CS8618
        public Customer() { }
        #pragma warning restore CS8618

        public Customer(Broker broker, string companyName, string firstName, string lastName, string email, string phone, string password)
          : base(email, firstName, lastName, email, phone, password)
        {
            ArgumentNullException.ThrowIfNull(broker);
            ArgumentException.ThrowIfNullOrEmpty(companyName);

            Broker = broker;
            CompanyName = companyName;
        }

        public string CompanyName { get; private set; }
        //public DateOnly DateOfBirth { get; private set; }
        //public string Gender { get; private set; }
        public KycInfo Kyc { get; private set; } = new();


        public virtual Broker Broker { get; private set; }
        //public virtual ICollection<Policy> Policies { get; private set; }
    }
}
