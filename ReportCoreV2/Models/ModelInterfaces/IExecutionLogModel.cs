using System.Collections.Generic;

namespace ReportCoreV2.Models.ModelInterfaces
{
    public interface IExecutionLogModel
    {
        List<ExecutionLogFields> ExecutionLogData { get; set; }
        List<ExecutionLogFieldsForExcel> ExecutionLogDataForExport { get; set; }
    }
}