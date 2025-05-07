namespace Notification_System.Models
{
    // Core abstraction for the Strategy Pattern in notification delivery.
    // Enables polymorphic notification delivery (Email/SMS/Push.)
    // Serves as the abstraction point for Dependency Injection
    public interface INotification
    {
        // Contract for sending a notification message
        void Send(string message);
    }
}
