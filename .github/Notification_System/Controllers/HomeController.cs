using Microsoft.AspNetCore.Mvc;
using Notification_System.Models;

namespace Notification_System.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string name, string selectedType)
        {
            var notification = NotificationFactory.CreateNotification(selectedType);
            var user = new User(name, notification);

            // Add the user to the singleton manager (or any other logic)
            NotificationManager.Instance.Subscribers.Add(user);

            ViewBag.Message = $"{name} subscribed to {selectedType} notifications.";
            return View();
        }

        [HttpGet]
        public IActionResult Notify()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Notify(string message)
        {
            NotificationManager.Instance.NotifyAll(message);
            ViewBag.Status = "Notification sent to all subscribers.";
            return View();
        }
    }
}
