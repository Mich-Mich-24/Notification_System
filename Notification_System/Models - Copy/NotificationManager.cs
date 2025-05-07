using Notification_System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Notification_System.Models;
using System.Runtime.ConstrainedExecution;

//Factory Pattern for NotificationFactory
//Strategy Pattern for  notification methods per-user
//Observer Pattern user.Update behavior
namespace Notification_System.Models
{
    // Manages notification subscribers and distribution
    public class NotificationManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IMyEmailSender _emailSender;

        // Active notification subscribers
        public List<User> Subscribers { get; set; } = new();

        // Initialize with database context and email service
        public NotificationManager(ApplicationDbContext context, IMyEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // Loads users and configures their notification methods
        public async Task InitializeSubscribersAsync()
        {
            var users = await _context.Users.ToListAsync();
            foreach (var user in users)
            {
                var notification = NotificationFactory.CreateNotification(
                    user.NotificationType,
                    _emailSender,
                    user.Email,
                    user.PhoneNumber,
                    user.PushId);

                user.SetNotificationMethod(notification);
                Subscribers.Add(user);
            }
        }

        public void NotifyAll(string message)
        {
            foreach (var user in Subscribers)
            {
                user.Update(message);
            }
        }
    }
}