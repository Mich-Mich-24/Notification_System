namespace Notification_System.Models
{
    //StrategyBase
    public interface INotification
    {
        void Send(string message);
    }
}
