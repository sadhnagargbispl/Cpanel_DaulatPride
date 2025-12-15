using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Daulatpride.Models;
using Daulatpride.Domain.Entities;
using Daulatpride.Domain.Interface;
using System.Runtime.InteropServices;
//using Daulatpride.Fillters;
namespace Daulatpride.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //private readonly I_Report ireport;
        ////private int CompNo;

        //public HomeController(ILogger<HomeController> logger, I_Report i_report)
        //{
        //    _logger = logger;
        //    ireport = i_report;
        //}
        private readonly ILogger<HomeController> _logger;

        // ? ONLY LOGGER
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
