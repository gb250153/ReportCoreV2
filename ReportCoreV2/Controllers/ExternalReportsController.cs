using Microsoft.AspNetCore.Mvc;
using ReportCoreV2.BusinessDataHandler;
using ReportCoreV2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ReportCoreV2.Helper;

namespace ReportCoreV2.Controllers
{
    public class ExternalReportsController : Controller
    {
        private readonly IExternalProjectDataHandler _externalProjectDataHandler;
        private IExternalDataAddViewModel _externalDataAddViewModel;
        private IExternalDataHandler _externalDataHandler;
        public ExternalReportsController(IExternalProjectDataHandler externalProjectDataHandler, IExternalDataAddViewModel externalDataAddViewModel, IExternalDataHandler externalDataHandler)
        {
            _externalProjectDataHandler = externalProjectDataHandler;
            _externalDataAddViewModel = externalDataAddViewModel;
            _externalDataHandler = externalDataHandler;
        }
        [NoDirectAccess]
        public IActionResult Index()
        {
            var viewModel = _externalDataHandler.GetExternalDashboard();
            return View(viewModel);
        }

        [NoDirectAccess]
        public IActionResult _AddExecution()
        {
            var viewModel = _externalProjectDataHandler.GetExternalProjectList();
            return PartialView(viewModel);
        }
        [NoDirectAccess]
        [HttpPost]
        public IActionResult _AddExecution(ExternalDataAddViewModel DataToAdd)
        {
            if (ModelState.IsValid)
            {
                _externalDataHandler.AddExecutionData(DataToAdd);


                return RedirectToAction("Index", "ExternalReports");
            }
            else
            {
                var viewModel = _externalProjectDataHandler.GetExternalProjectList();
                return View(viewModel);
            }
        }
        [NoDirectAccess]
        public IActionResult _AddScenarios()
        {
            var viewModel = _externalProjectDataHandler.GetExternalProjectList();
            return PartialView(viewModel);
        }
        [NoDirectAccess]
        [HttpPost]
        public IActionResult _AddScenarios(ExternalDataAddViewModel DataToAdd)
        {
            if (ModelState.IsValid)
            {
                _externalDataHandler.AddCreatedScenariosData(DataToAdd);


                return RedirectToAction("Index", "ExternalReports");
            }
            else
            {
                var viewModel = _externalProjectDataHandler.GetExternalProjectList();
                return View(viewModel);
            }
        }
        
    }
}
