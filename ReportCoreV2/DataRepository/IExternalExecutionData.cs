using ReportCoreV2.Models;
using System.Collections.Generic;

namespace ReportCoreV2.DataRepository
{
    public interface IExternalExecutionData
    {
        List<ExternalExecutionForDashboard> GetCurrentMonthExternalExecutionDataForDashboard();
        List<ExternalExecutionForDashboard> GetExternalExecutionByWeekForDashboards(string YearFilter);
        List<ExternalExecutionDataFields> GetExternalExecutionData(string YearFilter, string ProjectFilter);
        List<ExternalExecutionForDashboard> GetExternalExecutionDataForDashboard(string YearFilter);
        List<ExternalExecutionForDashboard> GetLastYearExternalExecutionDataForDashboard(string YearFilter);
        List<ExternalExecutionForDashboard> GutCurrentYearSumPerProject(string YearFilter);
        List<ExternalExecutionForDashboard> GutLastYearSumPerProject();
        
    }
}