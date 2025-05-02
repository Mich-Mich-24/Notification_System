namespace Notification_System.Models
{

    public interface INotyfService
    {
        void Error(string v);
        void Success(string v);
    }
}