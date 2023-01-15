using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
