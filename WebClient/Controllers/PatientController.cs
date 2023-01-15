using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
