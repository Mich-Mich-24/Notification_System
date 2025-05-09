// Models/EmailNotification.cs
namespace Notification_System.Models
{
    public class EmailNotification : INotification
    {
        private readonly IMyEmailSender _emailSender;
        private readonly string _recipientEmail;

        public EmailNotification(IMyEmailSender emailSender, string recipientEmail)
        {
            _emailSender = emailSender;
            _recipientEmail = recipientEmail;
        }

        public void Send(string message)
        {
            _emailSender.SendEmail(_recipientEmail, "Notification", message);
        }
    }
}