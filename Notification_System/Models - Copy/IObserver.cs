namespace Notification_System.Models
{
    // Observer Base Class
    // Defines the core contract for observer components in the Observer design pattern.

    public interface IObserver
    {

        // Receives and handles update notifications from a subscribed subject.
        // Sends message using concrete implementation
        void Update(string message);
    }
}
