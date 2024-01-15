namespace Gibs.Domain.Entities
{
    public class Insurer
    {
        #pragma warning disable CS8618
        public Insurer() { }
        #pragma warning restore CS8618

        public Insurer(string insurerId, string insurerName, ApiAuthEnum apiAuthStyle, string naicomId)
        {
            InsurerId = insurerId;
            InsurerName = insurerName;
            ApiAuthStyle = apiAuthStyle;
            CreatedUtc = DateTime.UtcNow;
            NaicomId = naicomId;
            IsActive = true;
        }

        public string InsurerId { get; private set; }
        public string InsurerName { get; private set; }
        public DateTime CreatedUtc { get; private set; }
        public ApiAuthEnum ApiAuthStyle { get; private set; }
        public string NaicomId { get; private set; }
        public bool IsActive { get; private set; }
    }

    public enum ApiAuthEnum
    {
        BEARER_TOKEN, JSON_WEB_TOKEN, QUERY_STRING
    }
}
