namespace Gibs.Portal
{
    public class Settings
    {
        public SMTPOptions SMTP { get; init; } = new();
        public PaystackOptions Paystack { get; init; } = new();

        //public class SmtpOptions(string host, int port, string userName, string password, string senderEmail)
        //{
        //    public string Host { get; set; } = host;
        //    public int Port { get; set; } = port;
        //    public string Username { get; set; } = userName;
        //    public string Password { get; set; } = password;
        //    public string SenderEmail { get; set; } = senderEmail;
        //}

        public class SMTPOptions
        {
            public string ServerEndpoint { get; init; } = string.Empty;
            public string DisplayName { get; init; } = string.Empty;
            public string Username { get; init; } = string.Empty;
            public string Password { get; init; } = string.Empty;
            public bool UseSSL { get; init; } = true;
        }

        public class PaystackOptions
        {
            public string Key { get; init; } = string.Empty;
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