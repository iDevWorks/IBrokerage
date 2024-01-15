using System.Security.Cryptography;

namespace Gibs.Domain.Entities
{
    public abstract class Person
    {
        #pragma warning disable CS8618
        protected Person() { }
        #pragma warning restore CS8618

        public Person(string id, string firstName, string lastName, string email, string phone, string password)
        {
            ArgumentException.ThrowIfNullOrEmpty(id);
            ArgumentException.ThrowIfNullOrEmpty(firstName);
            ArgumentException.ThrowIfNullOrEmpty(lastName);
            ArgumentException.ThrowIfNullOrEmpty(email);
            ArgumentException.ThrowIfNullOrEmpty(phone);
            ArgumentException.ThrowIfNullOrEmpty(password);

            //if (phone < 2347000000000 || phone > 2349999999999)
            //    throw new ArgumentOutOfRangeException(nameof(phone));

            Id = id;
            CreatedUtc = DateTime.Now;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Password = HashPassword(password);
            IsActive = false;
        }

        public void Enable(bool enable)
        {
            IsActive = enable;
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
        public DateTime CreatedUtc { get; private set; }
        public string? Title { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
