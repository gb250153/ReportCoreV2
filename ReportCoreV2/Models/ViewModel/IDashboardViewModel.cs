using System.Collections.Generic;

namespace ReportCoreV2.Models.ViewModel
{
    public interface IDashboardViewModel
    {
        List<DataPointsForGraphsViewModel> ApprovedScenarioLogByWeekDashboardDataForGraphs { get; set; }
        List<DataPointsForGraphsViewModel> ApprovedScenarioLogDashboardDataForGraphs { get; set; }
        List<DataPointsForGraphsViewModel> ExecutionLogCurrentmonthDashboardDataForGraphs { get; set; }
        List<DataPointsForGraphsViewModel> ExecutionLogDashboardDataForGraphs { get; set; }
        List<DataPointsForGraphsViewModel> LastYearApprovedScenarioLogDashboardDataForGraphs { get; set; }
        List<DataPointsForGraphsViewModel> LastYearExecutionLogDashboardDataForGraphs { get; set; }
        string LastYearofData { get; set; }
        string MonthName { get; set; }
        string YearofData { get; set; }
    }
}