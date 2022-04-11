using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportCoreV2.BusinessDataHandler;
using ReportCoreV2.DataRepository;
using ReportCoreV2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace ReportCoreV2.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IDashboardDataHandler _dashboardDataHandler;

        public HomeController(ILogger<HomeController> logger, IDashboardDataHandler dashboardDataHandler)
        {
            _logger = logger;

            _dashboardDataHandler = dashboardDataHandler;


        }

        public IActionResult Index()
        {
            

            return View(_dashboardDataHandler.GetDashboardData());
            
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
