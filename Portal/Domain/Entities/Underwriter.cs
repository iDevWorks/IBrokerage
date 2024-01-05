namespace Gibs.Domain.Entities
{
    public class Underwriter
    {
        public Underwriter() { }

        public Underwriter(string name, string address, string phone, string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(address);
            ArgumentException.ThrowIfNullOrWhiteSpace(phone);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);

            Name = name;
            Address = address;
            Phone = phone;
            Email = email;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; } 
        public string Phone { get; private set; }
        public string Email { get; private set; }

        public virtual ICollection<Policy> Policies { get; private set; }
    }
}
