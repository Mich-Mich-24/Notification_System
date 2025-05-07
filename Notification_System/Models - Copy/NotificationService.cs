namespace Notification_System.Models
{
    //ObserverSubject
    // Subject in Observer pattern - manages observer subscriptions and notifications
    public class NotificationService
    {
        private readonly List<IObserver> _observers = new();

        // Adds an observer to notification list
        public void Subscribe(IObserver observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }
        // Removes an observer from notification list
        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }
        // Notifies all subscribed observers
        public void NotifyAll(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);// Push update to each observer
            }
        }
    }
}
