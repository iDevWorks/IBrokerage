namespace Portal.Entities
{
    public class Client
    {
        public Client() { }

        public Client(Broker broker, Policy policy, string name, string address, string email, string phoneNumber) 
        {
            ArgumentNullException.ThrowIfNull(broker);
            ArgumentNullException.ThrowIfNull(policy);

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if(string.IsNullOrWhiteSpace(Address))
                throw new ArgumentNullException(nameof(address));

            if(string.IsNullOrWhiteSpace(Email))
                throw new ArgumentNullException(nameof(email));

            if(string.IsNullOrWhiteSpace(PhoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            Broker = broker;
            BrokerId = broker.Id;
            Name = name;
            Email = email;
            Address = address;
            PhoneNumber = phoneNumber;
            DateAdded = DateTimeOffset.UtcNow;

            Policies.Add(policy);
        }

        public string Id { get; private set; }
        public string BrokerId { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTimeOffset DateAdded { get; private set; }

        public virtual Broker Broker { get; private set; }
        public virtual ICollection<Policy> Policies { get; private set; } = new List<Policy>();
    }
}
