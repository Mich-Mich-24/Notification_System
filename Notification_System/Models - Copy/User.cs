using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Notification_System.Models
{
    public class User : IObserver
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int UserId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PushId { get; set; }

        [Required]
        public string NotificationType { get; set; } = string.Empty;

        [NotMapped]
        public INotification? Notification { get; set; }

        public void Update(string message)
        {
            Notification?.Send($"To {Name}: {message}");
        }

        public void SetNotificationMethod(INotification notification)
        {
            Notification = notification;
        }
    }
}
