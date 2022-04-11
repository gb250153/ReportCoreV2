using Microsoft.AspNetCore.Mvc;
using ReportCoreV2.BusinessDataHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ReportCoreV2.Helper;

namespace ReportCoreV2.Controllers
{
    public class ProjectDashboardController : Controller
    {
        private IProjectDashboardDataHandler _projectDashboardDataHandler;

        public ProjectDashboardController(IProjectDashboardDataHandler projectDashboardDataHandler)
        {
            _projectDashboardDataHandler = projectDashboardDataHandler;
        }
        [NoDirectAccess]
        public IActionResult Index()
        {
            //    var prj = _projectDashboardDataHandler.GetProjectList();
            var selected = "";
            var ViewModel = _projectDashboardDataHandler.MapToView(selected);
           // var viewModel = _projectDashboardDataHandler.GetProjectList();
            return View(ViewModel);
        }

        [NoDirectAccess]
        [HttpPost]
        public IActionResult Index(string selectedproject)
        {
            
          var  ViewModel = _projectDashboardDataHandler.MapToView(selectedproject);
           
            return View(ViewModel);
        }
    }
}
