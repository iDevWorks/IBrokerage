using System.Security.Cryptography;
using System.Text;

namespace Portal.Entities
{
    public class Broker
    {
        public Broker() { }

        public Broker(string email, string phoneNumber, string fullName, string address, string password) 
        {
            //ArgumentException.ThrowIfNullOrWhiteSpace(email);

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            if(string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException(nameof(phoneNumber));

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentNullException(nameof(fullName));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            Id = GenerateUniqueId(fullName, email);
            Email = email;
            PhoneNumber = phoneNumber;
            FullName = fullName;
            Address = address;
            CreatedDate = DateTimeOffset.UtcNow;
            Password = HashPassword(password);
        }

        public bool IsValidPassword(string password)
        {
            string hashedPassword = HashPassword(password);

            if (Password == hashedPassword) 
                return true;

            return false;
        }

        public void UpdatePassword(string oldPassword, string newPassword)
        {
            string hashedPassword = HashPassword(oldPassword);

            if (Password == hashedPassword)
            {
                Password = HashPassword(newPassword);
                return;
            }

            throw new Exception("Invalid old Password");
        }

        private static string HashPassword(string passwordToHash)
        {
            byte[] emptySalt = [];

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                passwordToHash,
                salt: emptySalt,
                iterations: 500,
                hashAlgorithm: HashAlgorithmName.SHA512,
                outputLength: 64);
            return Convert.ToHexString(hash);
        }

        public string Id { get; private set; }
        public string ConfirmationToken { get; set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string FullName { get; private set; }
        public string Address { get; private set; }
        public string Password { get; private set; }
        public DateTimeOffset CreatedDate { get; private set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Policy> Policies { get; set; }

        private static string GenerateUniqueId(string fullName, string email)
        {
            // Combine the name and email
            string combinedValue = $"{fullName}_{email}";

            // Hash the combined value to get a unique string
            byte[] hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(combinedValue));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
