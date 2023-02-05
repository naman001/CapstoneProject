using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMegaService _megaService;

        public HomeController(IMegaService megaService)
        {
            _megaService= megaService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _megaService.GetListAsync("codeMega");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var folderName = "egoData";
            await _megaService.UploadAsync(file, folderName);
            return Redirect("/");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFIle(string downloadUrl)
        {
            await _megaService.DeleteAsync(downloadUrl);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult>DownloadFile(string downloadUrl, string fileName)
        {
            var data = await _megaService.DownloadAsync(downloadUrl);

            return File(data, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
