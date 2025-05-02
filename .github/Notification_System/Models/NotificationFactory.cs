namespace Notification_System.Models
{
    public  static class NotificationFactory
    {
        public static INotification CreateNotification(string type)
        {
            return type switch
            {
                "Email" => new EmailNotification(),
                "SMS" => new SMSNotification(),
                "Push" => new PushNotification(),
                _ => throw new ArgumentException("Invalid notification type")
            };
        }

    }
}
