namespace Notification_System.Models
{
    public class NotificationManager
    {
        private static NotificationManager _instance;

        private static readonly object _lock = new ();

        public List<User> Subscribers = new();

        private NotificationManager() 
        { 
        }

        public static NotificationManager Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new NotificationManager();
                }
            }
        }

        public void NotifyAll(string message)
        {
            foreach(var user in Subscribers)
            {
                user.Notification.Send(message);
            }
        }
    }
}
