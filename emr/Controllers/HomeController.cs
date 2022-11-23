using emr.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace emr.Controllers
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
            ViewData["personName"] = HttpContext.Session.GetString("personName");
            if (ModelState.IsValid)
            {

            }
            //  ViewData["personName"] = HttpContext.Session.GetString("personName");
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