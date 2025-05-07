using Notification_System.Models;

namespace Notification_System.Models
{
    // Interface defining the contract for an email sending service.
    public interface IMyEmailSender
    { 
        // Sends an email to the specified recipient.
        void SendEmail(string to, string subject, string htmlMessage);
    }
}