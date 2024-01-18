namespace Gibs.Domain.Entities
{
    public class Underwriter
    {
        #pragma warning disable CS8618
        public Underwriter() { }
        #pragma warning restore CS8618

        public Underwriter(Insurer insurer, string apiKey1Username, string apiKey2Password)
        {
            ArgumentNullException.ThrowIfNull(insurer);
            ArgumentException.ThrowIfNullOrWhiteSpace(apiKey1Username);
            ArgumentException.ThrowIfNullOrWhiteSpace(apiKey2Password);

            Id = Guid.NewGuid().ToString();
            Insurer = insurer;
            ApiKey1Username = apiKey1Username;
            ApiKey2Password = apiKey2Password;
        }

        public string Id { get; private set; }
        public string? MappedFields { get; private set; }
        public string ApiKey1Username { get; private set; }
        public string ApiKey2Password { get; private set; } 

        public Insurer Insurer { get; private set; }
    }
}
