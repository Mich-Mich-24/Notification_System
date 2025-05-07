using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace Notification_System.Models
{
    public class MyEmailSender : IMyEmailSender
    {
        private readonly IConfiguration _configuration;

        public MyEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string to, string subject, string htmlMessage)
        {
            var fromEmail = _configuration["EmailSettings:Sender"] ?? throw new InvalidOperationException("Sender email not configured");
            var password = _configuration["EmailSettings:Password"] ?? throw new InvalidOperationException("Email password not configured");
            var smtpHost = _configuration["EmailSettings:SmtpHost"] ?? throw new InvalidOperationException("SMTP host not configured");
            var port = int.Parse(_configuration["EmailSettings:Port"] ?? throw new InvalidOperationException("Port not configured"));

            using var message = new MailMessage(fromEmail, to, subject, htmlMessage)
            {
                IsBodyHtml = true
            };

            using var client = new SmtpClient(smtpHost, port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail, password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            client.Send(message);
        }
    }
}