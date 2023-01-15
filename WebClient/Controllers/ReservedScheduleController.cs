using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class ReservedScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
