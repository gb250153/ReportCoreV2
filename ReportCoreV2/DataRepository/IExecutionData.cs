using ReportCoreV2.Models;
using System.Collections.Generic;

namespace ReportCoreV2.DataRepository
{
    public interface IExecutionData
    {
        List<ExecutionLogFieldsForExcel> GetExecutionDataForExport(string ProcessFilter, string YearFilter, string ProjectFilter);
        List<ExecutionLogFields> GetExecutionDataForGrid(string ProcessFilter, string YearFilter, string ProjectFilter);
    }
}