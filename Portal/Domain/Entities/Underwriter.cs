namespace Gibs.Domain.Entities
{
    public class Underwriter
    {
        public Underwriter() { }

        public Underwriter(string name, string address, string phoneNumber, string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(address);
            ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);

            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; } 
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }

        public virtual ICollection<Policy> Policies { get; private set; }
    }
}
