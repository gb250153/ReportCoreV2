using System.Collections.Generic;

namespace ReportCoreV2.Models.ViewModel
{
    public interface IExecutionLogViewModel
    {
        List<ExecutionLogFields> ExecutionLogDataForUi { get; set; }
        List<ExecutionLogFieldsForExcel> ExecutionLogDataForExport { get; set; }
    }
}