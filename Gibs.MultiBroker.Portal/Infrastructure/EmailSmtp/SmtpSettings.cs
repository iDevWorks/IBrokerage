namespace Gibs.Infrastructure.Email
{
    public class SmtpSettings(string host, int port, string userName, string password, string senderEmail)
    {
        public string Host { get; set; } = host;
        public int Port { get; set; } = port;
        public string UserName { get; set; } = userName;
        public string Password { get; set; } = password;
        public string SenderEmail { get; set; } = senderEmail;
    }

}
