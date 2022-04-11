using System.Collections.Generic;

namespace ReportCoreV2.Models.ViewModel
{
    public interface IExternalDashboardViewModel
    {
        List<DataPointsForGraphsViewModel> ExternalExecutionCurrentYearDashboardDataForGraphs { get; set; }
        string YearofData { get; set; }
        string LastYearofData { get; set; }
        string MonthName { get; set; }
        List<DataPointsForGraphsViewModel> ExternalCreationCurrentYearDashboardDataForGraphs { get; set; }
        List<DataPointsForGraphsViewModel> ExternalCreationByWeekDashboardDataForGraphs { get; set; }
        List<DataPointsForGraphsViewModel> ExternalExecutionByWeekDashboardDataForGraphs { get; set; }
    }
}