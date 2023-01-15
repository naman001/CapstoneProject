using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class TimeSlotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
