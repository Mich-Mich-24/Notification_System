using Microsoft.AspNetCore.Mvc;

namespace Notification_System.Controllers
{
    public class MainFormController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
