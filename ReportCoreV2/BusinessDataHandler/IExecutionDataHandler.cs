using ReportCoreV2.Models;
using ReportCoreV2.Models.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace ReportCoreV2.BusinessDataHandler
{
    public interface IExecutionDataHandler
    {
        DataTable ConvertToDataTable(List<ExecutionLogFieldsForExcel> executionLogDataForExcel);
        IExecutionLogViewModel GetDataAndTotalsForGrid();
        IExecutionLogViewModel GetDataAndTotalsForGridForExport();
    }
}