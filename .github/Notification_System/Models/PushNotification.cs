namespace Notification_System.Models
{
    public class PushNotification: INotification
    {
        public void Send(string message) => Console.WriteLine($"[SMS] {message}");
    }
}
