namespace Portal.Entities
{
    public class InsuranceCompany
    {
        public InsuranceCompany() { }

        public InsuranceCompany(string name, string address, string phoneNumber, string email)
        {
            if(string.IsNullOrWhiteSpace(Name)) 
                throw new ArgumentNullException(nameof(name));

            if(string.IsNullOrWhiteSpace(Address))
                throw new ArgumentNullException(nameof(address));

            if(string.IsNullOrWhiteSpace(PhoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            if(string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

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
