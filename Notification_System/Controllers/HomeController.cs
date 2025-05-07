using Microsoft.AspNetCore.Mvc;
using Notification_System.Models;
using Notification_System.Data;
using Microsoft.EntityFrameworkCore;

namespace Notification_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMyEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        private readonly NotificationManager _notificationManager;

        public HomeController(
            IMyEmailSender emailSender,
            ApplicationDbContext context,
            NotificationManager notificationManager) // Injected here
        {
            _emailSender = emailSender;
            _context = context;
            _notificationManager = notificationManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string name, string selectedType, string email, string phoneNumber, string pushId)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(selectedType))
            {
                ViewBag.Message = "Name and notification type are required.";
                return View();
            }

            var user = new User
            {
                Name = name,
                NotificationType = selectedType
            };

            switch (selectedType)
            {
                case "Email":
                    if (string.IsNullOrEmpty(email))
                    {
                        ViewBag.Message = "Email address is required for Email notifications.";
                        return View();
                    }
                    if (await _context.Users.AnyAsync(u => u.Email == email))
                    {
                        ViewBag.Message = "This email is already subscribed.";
                        return View();
                    }
                    user.Email = email;
                    break;

                case "SMS":
                    if (string.IsNullOrEmpty(phoneNumber))
                    {
                        ViewBag.Message = "Phone number is required for SMS notifications.";
                        return View();
                    }
                    if (await _context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber))
                    {
                        ViewBag.Message = "This phone number is already subscribed.";
                        return View();
                    }
                    user.PhoneNumber = phoneNumber;
                    break;

                case "Push":
                    if (string.IsNullOrEmpty(pushId))
                    {
                        ViewBag.Message = "Push ID is required for Push notifications.";
                        return View();
                    }
                    if (await _context.Users.AnyAsync(u => u.PushId == pushId))
                    {
                        ViewBag.Message = "This Push ID is already subscribed.";
                        return View();
                    }
                    user.PushId = pushId;
                    break;

                default:
                    ViewBag.Message = "Invalid notification type selected.";
                    return View();
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var notification = NotificationFactory.CreateNotification(
                selectedType,
                _emailSender,
                email,
                phoneNumber,
                pushId);

            user.SetNotificationMethod(notification);
            _notificationManager.Subscribers.Add(user);

            ViewBag.Message = $"{name} subscribed to {selectedType} notifications.";
            return View();
        }

        [HttpGet]
        public IActionResult Notify()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Notify(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                ViewBag.Status = "Message cannot be empty.";
                return View();
            }

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
                if (!_notificationManager.Subscribers.Any(u => u.UserId == user.UserId))
                {
                    _notificationManager.Subscribers.Add(user);
                }
            }

            _notificationManager.NotifyAll(message);
            ViewBag.Status = "Notification sent to all subscribers.";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManageSubscribers()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                var subscriber = _notificationManager.Subscribers.FirstOrDefault(u => u.UserId == id);
                if (subscriber != null)
                {
                    _notificationManager.Subscribers.Remove(subscriber);
                }
            }

            return RedirectToAction("ManageSubscribers");
        }
    }
}