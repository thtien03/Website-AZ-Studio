using DoAnMonHoc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DoAnMonHoc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
// wedding https://www.google.com.vn/search?q=%E1%BA%A3nh+header+studio+%C4%91%E1%BA%B9p&udm=2#imgrc=biy900SF5FhyIM&imgdii=W49ppJdDIi6GRM