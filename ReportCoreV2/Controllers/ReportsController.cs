using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using ClosedXML.Excel;
using ReportCoreV2.BusinessDataHandler;
using ReportCoreV2.Models;
using ReportCoreV2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using static ReportCoreV2.Helper;

namespace ReportCoreV2.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IExecutionDataHandler _executionDataHandler ;
        private readonly IApprovedScenarioDataHandler _approvedScenarioDataHandler;
        private readonly IDurationInfoDataHandler _durationInfoDataHandler;
        public ReportsController(IExecutionDataHandler executionDataHandler, IApprovedScenarioDataHandler approvedScenarioDataHandler, IDurationInfoDataHandler durationInfoDataHandler )
        {
            _executionDataHandler = executionDataHandler;
            _approvedScenarioDataHandler = approvedScenarioDataHandler;
            _durationInfoDataHandler = durationInfoDataHandler;
        }

        [NoDirectAccess]
        public IActionResult ExecutionReport()
        {
            return View(_executionDataHandler.GetDataAndTotalsForGrid());
        }
        [NoDirectAccess]
        public IActionResult ApprovedScenarioReport()
        {
            return View(_approvedScenarioDataHandler.GetDataAndTotalsForGrid());
        }
        [NoDirectAccess]
        public IActionResult DurationReport()
        {
            return View(_durationInfoDataHandler.GetDurationDataForUi());
        }
        [NoDirectAccess]
        public IActionResult ExportExecution()
        {
            return ExecutionExport("Execution Summery");
        }
        [NoDirectAccess]
        public IActionResult ExportApprovedScenario()
        {
            return ApprovedScenarioExport("Approved Scenario Summery");
        }
        [NoDirectAccess]
        public IActionResult ExportDurationInfo()
        {
            return DurationInfoExport("Duration Summery");
        }


        private FileContentResult ExecutionExport(string fileName)
        {
           
            
            IXLWorkbook wb = new XLWorkbook();
            //IXLWorksheet ws = wb.Worksheets.Add("Data");
            DataTable dt = _executionDataHandler.ConvertToDataTable(_executionDataHandler.GetDataAndTotalsForGridForExport().ExecutionLogDataForExport);
            dt.TableName = "Execution Summery";

            fileName = fileName + DateTime.Now.ToShortDateString() + ".xlsx";


            //Add DataTable in worksheet  
            wb.Worksheets.Add(dt);

            using MemoryStream stream = new MemoryStream();
            wb.SaveAs(stream);
            //Return xlsx Excel File  
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }

        private FileContentResult ApprovedScenarioExport(string fileName)
        {


            IXLWorkbook wb = new XLWorkbook();
            //IXLWorksheet ws = wb.Worksheets.Add("Data");
            DataTable dt = _approvedScenarioDataHandler.ConvertToDataTable(_approvedScenarioDataHandler.GetDataAndTotalsForGrid().ApprovedScenarioDataForUi);
            dt.TableName = "Approved Scenario Summery";

            fileName = fileName + DateTime.Now.ToShortDateString() + ".xlsx";


            //Add DataTable in worksheet  
            wb.Worksheets.Add(dt);

            using MemoryStream stream = new MemoryStream();
            wb.SaveAs(stream);
            //Return xlsx Excel File  
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }

        private FileContentResult DurationInfoExport(string fileName)
        {


            IXLWorkbook wb = new XLWorkbook();
            //IXLWorksheet ws = wb.Worksheets.Add("Data");
            DataTable dt = _durationInfoDataHandler.ConvertToDataTable(_durationInfoDataHandler.GetDurationDataForUi().DurationDataForUi);
            dt.TableName = "Duration Summery";

            fileName = fileName + DateTime.Now.ToShortDateString() + ".xlsx";


            //Add DataTable in worksheet  
            wb.Worksheets.Add(dt);

            using MemoryStream stream = new MemoryStream();
            wb.SaveAs(stream);
            //Return xlsx Excel File  
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }
    }
}
