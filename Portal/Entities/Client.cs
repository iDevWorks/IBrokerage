namespace Portal.Entities
{
    public class Client
    {
        public Client() { }

        public Client(Broker broker, string name, string address, string email, string phoneNumber) 
        {
            ArgumentNullException.ThrowIfNull(broker);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(address);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);

            Id = broker.Id;
            Broker = broker;
            BrokerId = broker.Id;
            Name = name;
            Email = email;
            Address = address;
            PhoneNumber = phoneNumber;
            DateAdded = DateTimeOffset.UtcNow;
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
