// Defines the namespace where the EmailNotification class resides
namespace Notification_System.Models
{
    // Implements the INotification interface for email-based notifications
    public class EmailNotification : INotification
    {
        // Dependency: Handles the actual email sending logic
        private readonly IMyEmailSender _emailSender;

        // Stores the recipient's email address
        private readonly string _recipientEmail;

        // Constructor: Initializes the EmailNotification with required dependencies
        // Parameters:
        //   - emailSender: Service responsible for sending emails
        //   - recipientEmail: The email address of the notification recipient
        public EmailNotification(IMyEmailSender emailSender, string recipientEmail)
        {
            _emailSender = emailSender;
            _recipientEmail = recipientEmail;
        }

        // Sends an email notification with the given message
        //   - message: The content of the notification to be sent
        public void Send(string message)
        {
            // Uses the injected email sender to dispatch the email
            _emailSender.SendEmail(_recipientEmail, "Notification", message);
        }
    }
}