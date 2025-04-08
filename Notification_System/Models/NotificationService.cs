namespace Notification_System.Models
{
    public class NotificationService
    {
        private readonly List<IObserver> _observers = new();

        public void Subscribe(IObserver observer) => _observers.Add(observer);

        public void  Unsubscribed(IObserver observer) => _observers.Remove(observer);

        public void NotifyAll(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }
    }
}
