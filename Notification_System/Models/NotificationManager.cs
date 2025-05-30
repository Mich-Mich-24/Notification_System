﻿using Notification_System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Notification_System.Models
{
    public class NotificationManager
    {
        private static NotificationManager? _instance;
        private static readonly object _lock = new();
        private readonly ApplicationDbContext _context;
        private readonly IMyEmailSender _emailSender;

        public List<User> Subscribers { get; set; } = new();

        private NotificationManager(ApplicationDbContext context, IMyEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
            InitializeSubscribers();
        }

        public static NotificationManager GetInstance(ApplicationDbContext context, IMyEmailSender emailSender)
        {
            lock (_lock)
            {
                return _instance ??= new NotificationManager(context, emailSender);
            }
        }

        private async void InitializeSubscribers()
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