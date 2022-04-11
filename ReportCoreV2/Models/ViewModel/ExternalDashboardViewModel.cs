using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models.ViewModel
{
    public class ExternalDashboardViewModel : IExternalDashboardViewModel
    {
        public List<DataPointsForGraphsViewModel> ExternalExecutionCurrentYearDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> ExternalCreationCurrentYearDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> ExternalCreationByWeekDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> ExternalExecutionByWeekDashboardDataForGraphs { get; set; }

        public string YearofData { get; set; }
        public string LastYearofData { get; set; }

        public string MonthName { get; set; }
    }
}
