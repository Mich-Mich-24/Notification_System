namespace Notification_System.Models
{
    public interface IObserver
    {
        void Update(string message);
    }
}
