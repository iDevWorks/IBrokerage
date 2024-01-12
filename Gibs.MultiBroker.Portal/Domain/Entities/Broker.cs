using System.Security.Cryptography;

namespace Gibs.Domain.Entities
{
    public class Broker
    {
        public Broker() { }

        public Broker(string email, string phone, string brokerName, string address, string password) 
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(phone);
            ArgumentException.ThrowIfNullOrWhiteSpace(brokerName);
            ArgumentException.ThrowIfNullOrWhiteSpace(address);
            ArgumentException.ThrowIfNullOrWhiteSpace(password);

            Email = email;
            Phone = phone;
            BrokerName = brokerName;
            Address = address;
            CreatedOn = DateTime.UtcNow;
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

        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string BrokerName { get; private set; }
        public string Address { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedOn { get; private set; }

    }
}
