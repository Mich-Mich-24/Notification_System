using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace Notification_System.Models
{
    // SMTP email sender implementing IMyEmailSender
    public class MyEmailSender : IMyEmailSender
    {
        private readonly IConfiguration _configuration;

        // Initialize with configuration (DI)
        public MyEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Sends HTML email via SMTP
        public void SendEmail(string to, string subject, string htmlMessage)
        {
            // Get settings from configuration
            var fromEmail = _configuration["EmailSettings:Sender"] ?? throw new InvalidOperationException("Sender email not configured");
            var password = _configuration["EmailSettings:Password"] ?? throw new InvalidOperationException("Email password not configured");
            var smtpHost = _configuration["EmailSettings:SmtpHost"] ?? throw new InvalidOperationException("SMTP host not configured");
            var port = int.Parse(_configuration["EmailSettings:Port"] ?? throw new InvalidOperationException("Port not configured"));

            // Configure email message
            using var message = new MailMessage(fromEmail, to, subject, htmlMessage)
            {
                IsBodyHtml = true  // Enable HTML content
            };

            // Setup SMTP client
            using var client = new SmtpClient(smtpHost, port)
            {
                EnableSsl = true,  // Secure connection
                Credentials = new NetworkCredential(fromEmail, password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            client.Send(message);  // Send email
        }
    }
}