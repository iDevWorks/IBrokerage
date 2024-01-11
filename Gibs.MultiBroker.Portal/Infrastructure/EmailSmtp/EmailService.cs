﻿using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace Gibs.Infrastructure.Email
{
    public class EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        private readonly SmtpSettings _smtpSettings = smtpSettings.Value;

        public Task SendEmailAsync(string senderEmail, string subject, string htmlMessage)
        {
            var client = new SmtpClient
            {
                Host = _smtpSettings.Host,
                Port = _smtpSettings.Port,
                Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password),
                EnableSsl = true,
            };

            var message = new MailMessage
            {
                From = new MailAddress(senderEmail), // Set sender's email
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };

            message.To.Add("omomowosymeon45@gmail.com"); // Set recipient's email Info@titpakinsurancebrokers.com

            return client.SendMailAsync(message);
        }
    }
}