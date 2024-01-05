namespace Gibs.Domain.Entities
{
    public class Client
    {
        public Client() { }

        public Client(string firstname, string lastname, string address, string email, string phone) 
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstname);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastname);
            ArgumentException.ThrowIfNullOrWhiteSpace(address);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(phone);

            Id = email;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Phone = phone;
            Address = address;
            CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public DateTime CreatedOn { get; private set; }
    }
}
