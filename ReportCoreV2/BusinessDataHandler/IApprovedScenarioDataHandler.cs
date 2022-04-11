using ReportCoreV2.Models;
using ReportCoreV2.Models.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace ReportCoreV2.BusinessDataHandler
{
    public interface IApprovedScenarioDataHandler
    {
        DataTable ConvertToDataTable(List<ApprovedScenariosFields> approvedScenarioForExcel);
        IApprovedScenarioViewModel GetDataAndTotalsForGrid();
    }
}