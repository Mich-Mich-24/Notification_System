namespace Notification_System.Models
{
    public class SMSNotification : INotification
    {
        public void Send(string message) => Console.WriteLine($"[Push] {message}");
    }
}
