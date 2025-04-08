namespace Notification_System.Models
{
    public class EmailNotification: INotification
    {
        public void Send (string message) => Console.WriteLine ($"[Email] {message}");
    }
}
