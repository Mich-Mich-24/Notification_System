namespace Notification_System.Models
{
    // Factory for creating notification instances
    public static class NotificationFactory
    {
        // Creates notification instance based on type
        public static INotification CreateNotification(
            string type,
            IMyEmailSender emailSender,
            string? recipientEmail,
            string? phoneNumber,
            string? pushId)
        {
            return type switch
            {
                // Email notification (requires valid email)
                "Email" when !string.IsNullOrEmpty(recipientEmail) =>
                    new EmailNotification(emailSender, recipientEmail),

                // SMS notification (requires phone number)
                "SMS" when !string.IsNullOrEmpty(phoneNumber) =>
                    new SMSNotification(phoneNumber),

                // Push notification (requires device ID)
                "Push" when !string.IsNullOrEmpty(pushId) =>
                    new PushNotification(pushId),

                // Invalid type/parameters
                _ => throw new ArgumentException("Invalid notification type or missing required parameter")
            };
        }
    }
}