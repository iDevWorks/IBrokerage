namespace Gibs.Domain.Entities
{
    public class Client
    {
        public Client() { }

        public Client(string name, string address, string email, string phone) 
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(address);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(phone);

            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public DateTime CreatedOn { get; private set; }
    }
}
