namespace Gibs.Domain.Entities
{
    public class Insured : Person
    {
        #pragma warning disable CS8618
        public Insured() { }
        #pragma warning restore CS8618

        public Insured(bool isCorporate, string? companyName, DateOnly dateOfBirthOrReg, 
            string firstName, string lastName, string email, string phone, string password)
          : base(email, firstName, lastName, email, phone, password)
        {
            if (isCorporate)
                ArgumentException.ThrowIfNullOrEmpty(companyName);
            ArgumentNullException.ThrowIfNull(dateOfBirthOrReg);

            IsCorporate = isCorporate;
            CompanyName = companyName;
            DateOfBirthOrReg = dateOfBirthOrReg;
        }

        public bool IsCorporate { get; private set; }
        public string? CompanyName { get; private set; }
        public DateOnly DateOfBirthOrReg { get; private set; }
        //public string Gender { get; private set; }
        public KycInfo Kyc { get; private set; } = new();
    }
}
