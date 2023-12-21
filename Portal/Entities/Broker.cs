using Microsoft.AspNetCore.Identity;

namespace Portal.Entities
{
    public class Broker : IdentityUser
    {
        public Broker() { }

        public Broker(string fullName, string address) 
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentNullException(nameof(fullName));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));

            FullName = fullName;
            Address = address;
            CreatedDate = DateTime.UtcNow;
        }

        public string FullName { get; private set; }
        public string Address {  get; private set; }
        public DateTime CreatedDate { get; private set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Policy> Policies { get; set; }
    }
}
