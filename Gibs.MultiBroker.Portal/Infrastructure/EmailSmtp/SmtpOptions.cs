namespace Gibs.Infrastructure.Email
{
    public class SmtpOptions(string host, int port, string userName, string password, string senderEmail)
    {
        public string Host { get; set; } = host;
        public int Port { get; set; } = port;
        public string Username { get; set; } = userName;
        public string Password { get; set; } = password;
        public string SenderEmail { get; set; } = senderEmail;
    }

}
