using LOG_Automation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LOG_Automation.Controllers
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
            return RedirectToAction("Login", "Auth");
        }

        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Captain()
        {
            return View();
        }

        public IActionResult Addplayers()
        {
            return View();
        }

        public IActionResult Player()
        {
            return View();
        }
    }
}
