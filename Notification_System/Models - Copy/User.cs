using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Notification_System.Models
{
    public class User : IObserver
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Db fields 
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        // Notification type identifiers
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PushId { get; set; }

        [Required]
        public string NotificationType { get; set; } = string.Empty;// Email/SMS/Push

        [NotMapped]
        public INotification? Notification { get; set; }

        // IObserver implementation - handles incoming notifications
        public void Update(string message)
        {
            Notification?.Send($"To {Name}: {message}");
        }
        // Sets the notification delivery strategy
        public void SetNotificationMethod(INotification notification)
        {
            Notification = notification;
        }
    }
}
