using ReportCoreV2.DataRepository;
using ReportCoreV2.Models.ModelInterfaces;
using ReportCoreV2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.BusinessDataHandler
{
    public class DashboardDataHandler : IDashboardDataHandler
    {
        private IDashboardModel _dashboardModel;
        private IDashboardData _dashboardData;

        private IDashboardViewModel _dashboardViewModel;

        public DashboardDataHandler(IDashboardModel dashboardModel, IDashboardData dashboardData, IDashboardViewModel dashboardViewModel)
        {
            _dashboardModel = dashboardModel;
            _dashboardData = dashboardData;
            _dashboardViewModel = dashboardViewModel;
        }
        private IDashboardModel GetLastYearExecutionLogDataForDashboardChart()
        {
            string Process = "Completed";
            int SelectedYear = DateTime.Today.Year;
            string lastyear = (SelectedYear - 1).ToString();
            
          _dashboardData.GetLastYearExecutionDataForDashboardChart(Process, lastyear);

            
            return _dashboardModel;

        }
        private IDashboardModel GetExecutionLogDataForDashboardChart()
        {
            string Process = "Completed";
            string SelectedYear = DateTime.Today.Year.ToString();
            
            _dashboardData.GetExecutionDataForDashboardChart(Process, SelectedYear);
           
            return _dashboardModel;

        }
        private IDashboardModel GetCurrentMonthExecutionLogDataForDashboardChart()
        {
            string Process = "Completed";

            _dashboardData.GetcurrentMonthExecutionDataForDashboardChart(Process);
            
            
            return _dashboardModel;

        }
        private IDashboardModel GetLastYearApprovedScenarioDataForDashboardChart()
        {
            string Process = "Approved";
            int SelectedYear = DateTime.Today.Year;
            string lastyear = (SelectedYear - 1).ToString();
            
            _dashboardData.GetLastYearApprovedScenarioDataForDashboardChart(Process, lastyear);

            
            return _dashboardModel;

        }
        private IDashboardModel GetApprovedScenarioDataForDashboardChart()
        {
            string Process = "Approved";
            string SelectedYear = DateTime.Today.Year.ToString();
            
             _dashboardData.GetApprovedScenarioDataForDashboardChart(Process, SelectedYear);

           
            return _dashboardModel;

        }
        private IDashboardModel GetApprovedScenarioByWeekDataForDashboardChart()
        {
            string Process = "Approved";
            string SelectedYear = DateTime.Today.Year.ToString();
            
             _dashboardData.GetApprovedScenarioDataByWeekForDashboardChart(Process, SelectedYear);

            
            return _dashboardModel;

        }




        private List<DataPointsForGraphsViewModel> GetLastYearDataForChartsWithNewDatapoints()
        {
            GetLastYearExecutionLogDataForDashboardChart();
            var listOfDataPoints = new List<DataPointsForGraphsViewModel>();
            var ProjectId = Guid.NewGuid();
            var PointsValues = new List<DataPoints>();
            string Total;
            var SortedList = _dashboardModel.LastYearExecutionLogDataForDashboard.OrderBy(p => p.Project);
            foreach (var item in SortedList)
            {

                // var Year = item.Year;

                PointsValues.Add(new DataPoints() { ColumnLabel = item.Project, ColumnValue = item.ProjectTotal });
            }
            Total = _dashboardModel.LastYearExecutionLogDataForDashboard.Sum(x => x.ProjectTotal).ToString();
            listOfDataPoints.Add(new DataPointsForGraphsViewModel() { DataPointsList = PointsValues, GuidID = ProjectId, ProjectTotal = Total });



            return listOfDataPoints;
        }
        private List<DataPointsForGraphsViewModel> GetDataForChartsWithNewDatapoints()
        {
            GetExecutionLogDataForDashboardChart();
            var listOfDataPoints = new List<DataPointsForGraphsViewModel>();
            var ProjectId = Guid.NewGuid();
            var PointsValues = new List<DataPoints>();
            string Total;
            var SortedList = _dashboardModel.ExecutionLogDataForDashboard.OrderBy(p => p.Project);
            foreach (var item in SortedList)
            {

                // var Year = item.Year;

                PointsValues.Add(new DataPoints() { ColumnLabel = item.Project, ColumnValue = item.ProjectTotal });
            }
            Total = _dashboardModel.ExecutionLogDataForDashboard.Sum(x => x.ProjectTotal).ToString();
            listOfDataPoints.Add(new DataPointsForGraphsViewModel() { DataPointsList = PointsValues, GuidID = ProjectId, ProjectTotal = Total });



            return listOfDataPoints;
        }
        private List<DataPointsForGraphsViewModel> GetCurrentMonthDataForChartsWithNewDatapoints()
        {
            GetCurrentMonthExecutionLogDataForDashboardChart();
            var listOfDataPoints = new List<DataPointsForGraphsViewModel>();
            var ProjectId = Guid.NewGuid();
            var PointsValues = new List<DataPoints>();
            string Total;
            var SortedList = _dashboardModel.CurrentMonthExecutionLogDataForDashboard.OrderBy(p => p.Project);
            foreach (var item in SortedList)
            {

                PointsValues.Add(new DataPoints() { ColumnLabel = item.Project, ColumnValue = item.ProjectTotal });

            }

            Total = _dashboardModel.CurrentMonthExecutionLogDataForDashboard.Sum(x => x.ProjectTotal).ToString();
            listOfDataPoints.Add(new DataPointsForGraphsViewModel() { DataPointsList = PointsValues, GuidID = ProjectId, ProjectTotal = Total });

            return listOfDataPoints;
        }

        private List<DataPointsForGraphsViewModel> GetLastYearApprovedScenarioDataForChartsWithNewDatapoints()
        {
            GetLastYearApprovedScenarioDataForDashboardChart();
            var listOfDataPoints = new List<DataPointsForGraphsViewModel>();
            var ProjectId = Guid.NewGuid();
            var PointsValues = new List<DataPoints>();
            string Total;
            var SortedList = _dashboardModel.ApprovedLastyearScenariosDataForDashboard.OrderBy(p => p.Project);
            foreach (var item in SortedList)
            {

                // var Year = item.Year;

                PointsValues.Add(new DataPoints() { ColumnLabel = item.Project, ColumnValue = item.ProjectTotal });
            }
            Total = _dashboardModel.ApprovedLastyearScenariosDataForDashboard.Sum(x => x.ProjectTotal).ToString();
            listOfDataPoints.Add(new DataPointsForGraphsViewModel() { DataPointsList = PointsValues, GuidID = ProjectId, ProjectTotal = Total });



            return listOfDataPoints;
        }
        private List<DataPointsForGraphsViewModel> GetApprovedScenarioDataForChartsWithNewDatapoints()
        {
            GetApprovedScenarioDataForDashboardChart();
            var listOfDataPoints = new List<DataPointsForGraphsViewModel>();
            var ProjectId = Guid.NewGuid();
            var PointsValues = new List<DataPoints>();
            string Total;
            var SortedList = _dashboardModel.ApprovedScenariosDataForDashboard.OrderBy(p => p.Project);
            foreach (var item in SortedList)
            {

                // var Year = item.Year;

                PointsValues.Add(new DataPoints() { ColumnLabel = item.Project, ColumnValue = item.ProjectTotal });
            }
            Total = _dashboardModel.ApprovedScenariosDataForDashboard.Sum(x => x.ProjectTotal).ToString();
            listOfDataPoints.Add(new DataPointsForGraphsViewModel() { DataPointsList = PointsValues, GuidID = ProjectId, ProjectTotal = Total });



            return listOfDataPoints;
        }
        private List<DataPointsForGraphsViewModel> GetApprovedScenarioByWeekDataForChartsWithNewDatapoints()
        {
            GetApprovedScenarioByWeekDataForDashboardChart();
            var listOfDataPoints = new List<DataPointsForGraphsViewModel>();
            var ProjectId = Guid.NewGuid();
            var PointsValues = new List<DataPoints>();

            string Total;
            var SortedList = _dashboardModel.ApprovedScenariosDataForDashboard.OrderBy(p => p.Project);

            foreach (var item in _dashboardModel.ApprovedScenariosByWeekDataForDashboard)
            {

                PointsValues.Add(new DataPoints() { ColumnLabel = ("Week " + item.NumberOfWeek), ColumnValue = item.ProjectTotal });


                //PointsValues.Add(new DataPoints() { ColumnLabel = item.DateOfTotal.Date.ToString("dd/MM/yyyy"), ColumnValue = item.ProjectTotal });
            }

            var AggregatePointsValue = PointsValues.GroupBy(c => c.ColumnLabel).Select(x => new DataPoints { ColumnLabel = x.Key, ColumnValue = x.Sum(s => s.ColumnValue) });

            Total = _dashboardModel.ApprovedScenariosByWeekDataForDashboard.Sum(x => x.ProjectTotal).ToString();
            listOfDataPoints.Add(new DataPointsForGraphsViewModel() { DataPointsList = AggregatePointsValue.ToList(), GuidID = ProjectId, ProjectTotal = Total });



            return listOfDataPoints;
        }

        private IDashboardViewModel MapModelToViewModel()
        {
            //DashboardModel dashboardModel
            int lastyear = DateTime.Today.Year;
            

            _dashboardViewModel.LastYearExecutionLogDashboardDataForGraphs = GetLastYearDataForChartsWithNewDatapoints();
            _dashboardViewModel.ExecutionLogDashboardDataForGraphs = GetDataForChartsWithNewDatapoints();
            _dashboardViewModel.ExecutionLogCurrentmonthDashboardDataForGraphs = GetCurrentMonthDataForChartsWithNewDatapoints();
            _dashboardViewModel.ApprovedScenarioLogDashboardDataForGraphs = GetApprovedScenarioDataForChartsWithNewDatapoints();
            _dashboardViewModel.LastYearApprovedScenarioLogDashboardDataForGraphs = GetLastYearApprovedScenarioDataForChartsWithNewDatapoints();
            _dashboardViewModel.ApprovedScenarioLogByWeekDashboardDataForGraphs = GetApprovedScenarioByWeekDataForChartsWithNewDatapoints();
            _dashboardViewModel.YearofData = DateTime.Today.Year.ToString(); ;
            _dashboardViewModel.LastYearofData = (lastyear - 1).ToString();
            _dashboardViewModel.MonthName = DateTime.Today.ToString("MMMM");
            return _dashboardViewModel;
        }

        public IDashboardViewModel GetDashboardData()
        {



            MapModelToViewModel();

            return _dashboardViewModel;
        }

    }
}
