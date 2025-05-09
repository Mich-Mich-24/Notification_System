namespace Notification_System.Models
{
    public class SMSNotification : INotification
    {
        private readonly string _phoneNumber;

        public SMSNotification(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        public void Send(string message)
        {
            // Implementation for sending SMS
            Console.WriteLine($"Sending SMS to {_phoneNumber}: {message}");
        }
    }
}