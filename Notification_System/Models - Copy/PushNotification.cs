namespace Notification_System.Models
{
    public class PushNotification : INotification
    {
        private readonly string _pushId;

        public PushNotification(string pushId)
        {
            _pushId = pushId;
        }

        public void Send(string message)
        {
            // Implementation for push notification
            Console.WriteLine($"Sending push notification to {_pushId}: {message}");
        }
    }
}