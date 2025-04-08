namespace Notification_System.Models
{
        public class User : IObserver
        {
            public string Name { get; set; }
            public INotification Notification { get; set; }

            public User(string name, INotification notification)
            {
                Name = name;
                Notification = notification;
            }

            public void Update(string message)
            {
                Notification.Send($"To {Name}: {message}");
            }

            public void SetNotificationMethod(INotification notification)
            {
                Notification = notification;
            }
        }

    }

