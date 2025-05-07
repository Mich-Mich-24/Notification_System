// Models/NotificationManager.cs
using Notification_System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Notification_System.Models
{
    public class NotificationManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IMyEmailSender _emailSender;

        public List<User> Subscribers { get; set; } = new();

        public NotificationManager(ApplicationDbContext context, IMyEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

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