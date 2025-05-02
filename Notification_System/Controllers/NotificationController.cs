using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NToastNotify;
using Notification_System.Models.NotificationSystem.Hubs;
using Notification_System.Models;


namespace NotificationSystem.Controllers
{
    public class NotificationController : Controller
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly Notification_System.Models.INotyfService _notyfService;

        public NotificationController(IHubContext<NotificationHub> hubContext, INotyfService notyfService)
        {
            _hubContext = hubContext;
            _notyfService = notyfService;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                _notyfService.Error("Message cannot be empty.");
                return RedirectToAction("Index");
            }

            // Broadcast message to all connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);

            // Display a toast notification
            _notyfService.Success("Notification sent successfully.");

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
       
   
}
