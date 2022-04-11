using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models.ViewModel
{
    public class DashboardViewModel : IDashboardViewModel
    {
        public List<DataPointsForGraphsViewModel> LastYearExecutionLogDashboardDataForGraphs { get; set; }

        public List<DataPointsForGraphsViewModel> ExecutionLogCurrentmonthDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> LastYearApprovedScenarioLogDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> ApprovedScenarioLogDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> ApprovedScenarioLogByWeekDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> ExecutionLogDashboardDataForGraphs { get; set; }

        public string YearofData { get; set; }
        public string LastYearofData { get; set; }

        public string MonthName { get; set; }
    }
}
