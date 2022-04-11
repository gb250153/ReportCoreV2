using ReportCoreV2.Models;
using System.Collections.Generic;

namespace ReportCoreV2.DataRepository
{
    public interface IDashboardData
    {
        List<ExecutionLogFieldsForDashboard> GetApprovedScenarioDataByWeekForDashboardChart(string ProcessFilter, string YearFilter);
        List<ExecutionLogFieldsForDashboard> GetApprovedScenarioDataForDashboardChart(string ProcessFilter, string YearFilter);
        List<ExecutionLogFieldsForDashboard> GetcurrentMonthExecutionDataForDashboardChart(string ProcessFilter);
        List<ExecutionLogFieldsForDashboard> GetExecutionDataForDashboardChart(string ProcessFilter, string YearFilter);
        List<ExecutionLogFieldsForDashboard> GetLastYearApprovedScenarioDataForDashboardChart(string ProcessFilter, string YearFilter);
        List<ExecutionLogFieldsForDashboard> GetLastYearExecutionDataForDashboardChart(string ProcessFilter, string YearFilter);
        
    }
}