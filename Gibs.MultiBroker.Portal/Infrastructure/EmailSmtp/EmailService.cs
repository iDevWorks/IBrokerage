using System.Net;
using System.Net.Mail;
using System.Globalization;
using Gibs.Portal;

namespace Gibs.Infrastructure.Email
{
    public class EmailService
    {
        private readonly SmtpClient _client;
        private readonly MailMessage _message;

        public EmailService(Settings settings)
        {
            var smtp = settings.SMTP;
            var (host, port) = ParseEndpoint(smtp.ServerEndpoint);

            _client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(smtp.Username, smtp.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = smtp.UseSSL,
                Timeout = 300000
            };

            _message = new MailMessage
            {
                IsBodyHtml = false,
                Sender = new MailAddress(smtp.Username, "AVERTY-NAAANU"),
                From = new MailAddress(smtp.Username, smtp.DisplayName),
            };

            //if (settings.Users.Debug.Length > 0)
            //    _message.Bcc.Add(settings.Users.Debug[0]);
        }

        private static (string Host, int Port) ParseEndpoint(string serverEndpoint)
        {
            var port = 25;
            var parts = serverEndpoint.Split(':');

            if (parts.Length > 0)
                port = int.Parse(parts[1]);

            return (parts[0], port);
        }

        public Task TestEmailSettings(string mailTo, string mailSubject, string mailBody)
        {
            _message.Subject = mailSubject;
            _message.Body = mailBody;

            _message.To.Add(mailTo);
            return _client.SendMailAsync(_message);
        }

        private string GetEmailHeader(string caption, string domain = "SMSLive247.com")
        {
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            caption = textInfo.ToTitleCase(caption.ToLower());

            if (string.IsNullOrEmpty(_message.Subject))
                _message.Subject = caption;

            var x = $@"
            --------------------------------------------------------------------------------
            {domain}
            {caption}
            Date: {string.Format("{0:dd/MM/yyyy hh:mm:ss tt} UTC", DateTime.UtcNow)}
            --------------------------------------------------------------------------------";
            return x;
        }



        //public Task SendMailForgotPwd(Account member)
        //{
        //    string x = $@"
        //    {GetEmailHeader("PASSWORD RECOVERY")}

        //    Dear {member.FirstName},

        //    You have requested that your password be sent to you by email.

        //    Username/Email.......: {member.Username}
        //    Password.............: {member.Password}
        //    SMS Balance..........: {FormatNumber(member.Balance)}

        //    IMPORTANT: To ensure the safety of your account, we strongly recommend
        //    you change your password the next time you login to SMSLive247 website.";

        //    return SendEmailWithFooter(member.Username, x);
        //}

        public Task SendContactForm(string sender, string subject, string message, string email)
        {
            string x = $@"
            {GetEmailHeader("SMS DELIVERY UPDATE", subject)}

            Dear {sender},

            This is an automated response form the SMS gateway.

            You requested SMS delivery notification to this email, here it is:

            {message}

            --------------------------------------------------------------------------------
            Copyright {DateTime.Today.Year} All Rights Reserved.";

            return SendEmailWithFooter(email, x, false);
        }

        //public Task SendMailBulkSending(Account member, Message message, string status)
        //{
        //    string x = $@"
        //    {GetEmailHeader("BULK SMS SENDING...", "")}

        //    Dear {member.FirstName},

        //    Our BULK SMS service has recieved a request from your account.

        //    Status...............: {status}

        //    Sender ID............: {message.SenderID}
        //    Delivery Email.......: {message._ownerEmail}
        //    Message Text.........: {message.MessageText}

        //    Your current account balance is shown below:

        //    Username/Email.......: {member.Username}
        //    SMS Balance..........: {FormatNumber(member.Balance)}";

        //    return SendEmailWithFooter(member.Username, x);
        //}


        private Task SendEmailWithFooter(string emailTo, string body, bool addFooter = true)
        {
            if (addFooter)
            {
                string x = $@"
                If you have any questions or need technical assitance,
                support is available here: http://new.smslive247.com/support/ticket.aspx
                
                Or mail to:
                iDevWorks Technologies Ltd
                12 Samota Falola Street
                Ikeja, Lagos
                Nigeria
                
                Thank you for using SMSLive247!
                
                SMSLive247.com Team
                
                NOTE:
                Please do not reply to this message, which was sent from an unmonitored 
                e-mail address.
                Mail sent to this address cannot be answered.
                
                This message was sent to {emailTo}
                Remove yourself from future email here:
                http://new.smslive247.com/remove.aspx?me={emailTo}
                or reply to this message with the word 'remove' in your email message subject line.
                
                --------------------------------------------------------------------------------
                © Copyright {DateTime.Today.Year} SMSLive247.com. All Rights Reserved.";

                body += x;
            }

            _message.Body = body;
            _message.To.Add(emailTo);

            return _client.SendMailAsync(_message);
        }

        private static string FormatNumber(object amount)
        {
            // Return FormatNumber(Num, DecimalPlaces, True, False, TriState.True)
            return string.Format("{0:#,##0}", amount);
        }

        private static string FormatMoney(object amount, string symbol = "&#8358;&nbsp;")
        {
            return string.Format("{0} {1:#,##0.00}", symbol, amount);
        }
    }
}
