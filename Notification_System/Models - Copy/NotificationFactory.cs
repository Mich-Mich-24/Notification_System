// Models/NotificationFactory.cs
namespace Notification_System.Models
{
    public static class NotificationFactory
    {
        public static INotification CreateNotification(
            string type,
            IMyEmailSender emailSender,
            string? recipientEmail,
            string? phoneNumber,
            string? pushId)
        {
            return type switch
            {
                "Email" when !string.IsNullOrEmpty(recipientEmail) =>
                    new EmailNotification(emailSender, recipientEmail),
                "SMS" when !string.IsNullOrEmpty(phoneNumber) =>
                    new SMSNotification(phoneNumber),
                "Push" when !string.IsNullOrEmpty(pushId) =>
                    new PushNotification(pushId),
                _ => throw new ArgumentException("Invalid notification type")
            };
        }
    }
}