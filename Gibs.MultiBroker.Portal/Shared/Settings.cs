namespace Gibs.Portal
{
    public class Settings
    {
        public JWTOptions JWT { get; init; } = new();
        public SMSOptions SMS { get; init; } = new();
        public SMTPOptions SMTP { get; init; } = new();
        public ConnectionStringOptions ConnStrings { get; init; } = new();

        public class ConnectionStringOptions
        {
            public string ServiceBus { get; init; } = string.Empty;
            public string Storage { get; init; } = string.Empty;
            public string SqlDb { get; init; } = string.Empty;
        }

        public class SMSOptions
        {
            public long Free { get; init; } = 10;
            public string FreeText { get; init; } = string.Empty;
            public long MaxLength { get; init; } = 810;
            public long BalEmailsPerHr { get; init; } = 1000;
        }

        public class JWTOptions
        {
            public string Secret { get; init; } = string.Empty;
            public int ExpiresIn { get; init; } = 3600;
        }

        public class SMTPOptions
        {
            public string ServerEndpoint { get; init; } = string.Empty;
            public string DisplayName { get; init; } = string.Empty;
            public string Username { get; init; } = string.Empty;
            public string Password { get; init; } = string.Empty;
            public bool UseSSL { get; init; } = true;
        }

        public static class Headers
        {
            public const string PAGE_SIZE = "X-Page-Size";
            public const string PAGE_NUMBER = "X-Page-Number";
            public const string TOTAL_PAGES = "X-Total-Pages";
            public const string TOTAL_ITEMS = "X-Total-Count";
        }
    }
}