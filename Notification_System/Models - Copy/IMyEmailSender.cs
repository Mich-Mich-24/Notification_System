using Notification_System.Models;

// Models/IMyEmailSender.cs
namespace Notification_System.Models
{
    public interface IMyEmailSender
    {
        void SendEmail(string to, string subject, string htmlMessage);
    }
}