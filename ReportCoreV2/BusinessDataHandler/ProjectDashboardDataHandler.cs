using ReportCoreV2.DataRepository;
using ReportCoreV2.Models;
using ReportCoreV2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.BusinessDataHandler
{
    public class ProjectDashboardDataHandler : IProjectDashboardDataHandler
    {
        private IProjectDashboardModel _projectDashboardModel;

        private IProjectDashboardData _projectDashboardData;
        private IProjectDashboardViewModel _projectDashboardViewModel;

        public ProjectDashboardDataHandler(IProjectDashboardModel projectDashboardModel, IProjectDashboardData projectDashboardData, IProjectDashboardViewModel projectDashboardViewModel)
        {
            _projectDashboardModel = projectDashboardModel;
            _projectDashboardData = projectDashboardData;
            _projectDashboardViewModel = projectDashboardViewModel;
        }

        public IProjectDashboardViewModel GetProjectList()
        {
            _projectDashboardViewModel.ProjectsData = _projectDashboardData.GetProjectsListForDropdown();

            return _projectDashboardViewModel;
        }

        private IProjectDashboardModel GetSumDataForDashboard(string ProjectFilter)
        {
            _projectDashboardData.GetCurrentMonthTotalScenarioCreated(ProjectFilter);
            _projectDashboardData.GetLastYearCurrentMonthTotalScenarioCreated(ProjectFilter);
            _projectDashboardData.GetCurrentYearTotalScenarioCreated(ProjectFilter);
            _projectDashboardData.GetLastYearTotalScenarioCreated(ProjectFilter);

            _projectDashboardData.GetCurrentYearTotalScenarioExecution(ProjectFilter);
            _projectDashboardData.GetLastYearTotalScenarioExecution(ProjectFilter);
            _projectDashboardData.GetCurrentMonthTotalScenarioExecution(ProjectFilter);
            _projectDashboardData.GetLastYearCurrentMonthTotalScenarioExecution(ProjectFilter);
            
            
            _projectDashboardData.GetCurrentMonthTotalScenarioMaintain(ProjectFilter);
            _projectDashboardData.GetCurrentYearTotalScenarioMaintain(ProjectFilter);
            _projectDashboardData.GetLastyearCurrentMonthTotalScenarioMaintain(ProjectFilter);
            _projectDashboardData.GetLastYearTotalScenarioMaintain(ProjectFilter);

            _projectDashboardData.GetCycleDataForProjectDashboard(ProjectFilter);
            _projectDashboardData.GetProjectTotalScenarios(ProjectFilter);
            _projectDashboardData.GetProjectCreatedByList(ProjectFilter);

            return _projectDashboardModel;
        }

        private List<DataPointsForGraphsViewModel> GetCycleProjectDataForChartsWithNewDatapoints()
        {
            var listOfDataPoints = new List<DataPointsForGraphsViewModel>();


            foreach (var item in _projectDashboardModel.CycleResultDataForProjectDashboard)
            {
                var CycleId = item.Cycleid;
                //double Rate;
                double Passrate;
                var CycleName = item.CycleName;
                var dateOfCurrentIteration = item.Timestemp;
                double PassScenarios = item.PassScenario;

                double TotalScenario = item.ScenarioCountInCycle;
                if (TotalScenario != 0)
                {
                    //Rate = ((PassScenarios / TotalScenario) * 100);
                    Passrate = Math.Round(((PassScenarios / TotalScenario) * 100), 2);


                }
                else
                {
                    Passrate = 0;
                }

                var PointsValues = new List<DataPoints>();
                PointsValues.Add(new DataPoints() { ColumnLabel = "passed", ColumnValue = item.PassScenario });
                PointsValues.Add(new DataPoints() { ColumnLabel = "failed", ColumnValue = item.FailScenario });
                PointsValues.Add(new DataPoints() { ColumnLabel = "flowError", ColumnValue = item.FlowErrorScenario });
                PointsValues.Add(new DataPoints() { ColumnLabel = "technicalError", ColumnValue = item.TechErrorScenario });

                listOfDataPoints.Add(new DataPointsForGraphsViewModel() { DataPointsList = PointsValues, DateOfData = DateTime.Parse(dateOfCurrentIteration), GuidID = CycleId, PassScenariosinCycle = item.PassScenario.ToString(), TotalScenariosinCycle = TotalScenario.ToString(), Passrate = Passrate.ToString(), CycleNameForGraph = CycleName });

            }

            return listOfDataPoints;
        }

        private List<DataPointsForGraphsViewModel> GetScenarioTypeChartsWithNewDatapoints()
        {

            var listOfDataPoints = new List<DataPointsForGraphsViewModel>();
            var ProjectId = Guid.NewGuid();
            var PointsValues = new List<DataPoints>();
            var Sanity = Convert.ToDouble(_projectDashboardViewModel.ScenarioTypeSanity);
            var Regression = Convert.ToDouble(_projectDashboardViewModel.ScenarioTypeRegression);
            //var SortedList = _dashboardModel.ExecutionLogDataForDashboard.OrderBy(p => p.Project);

            // var Year = item.Year;

            PointsValues.Add(new DataPoints() { ColumnLabel = "Sanity", ColumnValue = Sanity });
            PointsValues.Add(new DataPoints() { ColumnLabel = "Regression", ColumnValue = Regression });


            listOfDataPoints.Add(new DataPointsForGraphsViewModel() { DataPointsList = PointsValues, GuidID = ProjectId });



            return listOfDataPoints;
        }

        private List<DataPointsForGraphsViewModel> GetAppLinkDataForChartsWithNewDatapoints()
        {
           
            var listOfDataPoints = new List<DataPointsForGraphsViewModel>();
            var ProjectId = Guid.NewGuid();
            var PointsValues = new List<DataPoints>();
            //string Total;
            
            var SortedList = _projectDashboardModel.projectAppLinks.OrderBy(p => p.Name);
            foreach (var item in SortedList)
            {

                // var Year = item.Year;

                PointsValues.Add(new DataPoints() { ColumnLabel = item.Name, ColumnValue = Convert.ToDouble(item.Amount) });
            }
            //Total = _projectDashboardModel.projectAppLinks.Sum(x => x.ProjectTotal).ToString();
            listOfDataPoints.Add(new DataPointsForGraphsViewModel() { DataPointsList = PointsValues, GuidID = ProjectId/*, ProjectTotal = Total*/ });



            return listOfDataPoints;
        }
        public IProjectDashboardViewModel MapToView(string ProjectFilter)
        {
            if (string.IsNullOrEmpty(ProjectFilter))
            {
                GetProjectList();
                ProjectFilter = _projectDashboardViewModel.ProjectsData.Select(m => m.Project).FirstOrDefault().ToString();
                                 
            }
            GetSumDataForDashboard(ProjectFilter);
            GetProjectList();

            _projectDashboardViewModel.TotalOfCurrentMonthExecution = _projectDashboardModel.TotalExecutedScenarioInCurrentMonth;
            _projectDashboardViewModel.TotalOfCurrentYearExecution = _projectDashboardModel.TotalExecutedScenarioInCurrentYear;
            _projectDashboardViewModel.TotalOfCurrentYearCreated = _projectDashboardModel.TotalCreatedScenarioInCurrentYear;
            _projectDashboardViewModel.TotalOfCurrentMonthCreated = _projectDashboardModel.TotalCreatedScenarioInCurrentMonth;
            _projectDashboardViewModel.TotalOfCurrentYearMaintain = _projectDashboardModel.TotalMaintainScenarioInCurrentYear;
            _projectDashboardViewModel.TotalOfCurrentMonthMaintain = _projectDashboardModel.TotalMaintainScenarioInCurrentMonth;
            _projectDashboardViewModel.CycleDataProjectDashboardDataForGraphs = GetCycleProjectDataForChartsWithNewDatapoints();
            _projectDashboardViewModel.ProjectAppLinks = _projectDashboardModel.projectAppLinks;
            _projectDashboardViewModel.TotalScenariosInProject = _projectDashboardModel.TotalScenariosInProject;
            _projectDashboardViewModel.ScenarioTypeRegression = _projectDashboardModel.ScenarioTypeRegression;
            _projectDashboardViewModel.ScenarioTypeSanity = _projectDashboardModel.ScenarioTypeSanity;
            _projectDashboardViewModel.TotalOfLastYearCurrentMonthExecution = _projectDashboardModel.TotalLastYearExecutedScenarioInCurrentMonth;
            _projectDashboardViewModel.TotalOfLastYearExecution = _projectDashboardModel.TotalExecutedScenarioInLastYear;
            _projectDashboardViewModel.TotalOfLastYearCurrentMonthCreated = _projectDashboardModel.TotalCreatedScenarioInLastYearCurrentMonth;
            _projectDashboardViewModel.TotalOfLastYearCreated = _projectDashboardModel.TotalCreatedScenarioInLastYear;
            _projectDashboardViewModel.TotalOfLastYearCurrentMonthMaintain = _projectDashboardModel.TotalMaintainScenarioInLastYearCurrentMonth;
            _projectDashboardViewModel.TotalOfLastYearMaintain = _projectDashboardModel.TotalMaintainScenarioInLastYear;
            _projectDashboardViewModel.ScenarioTypeProjectDashboardDataForGraphs = GetScenarioTypeChartsWithNewDatapoints();
            _projectDashboardViewModel.ProjectUsersList = _projectDashboardModel.ProjectScenarioCreatedBy;
            _projectDashboardViewModel.ProjectAppLinksDashboardDataForGraphs = GetAppLinkDataForChartsWithNewDatapoints();
            
            _projectDashboardViewModel.MonthName = DateTime.Today.ToString("MMMM");
            _projectDashboardViewModel.YearofData = DateTime.Today.Year.ToString();
            _projectDashboardViewModel.LastYearofData = DateTime.Today.AddYears(-1).Year.ToString();
            _projectDashboardViewModel.ProjectName = ProjectFilter;

            return _projectDashboardViewModel;
        }
       

      
        private IProjectDashboardData FilteredProjectUsersList(string ProjectFilter)
        {
            _projectDashboardData.GetProjectCreatedByList(ProjectFilter);
            return _projectDashboardData;
        }
    }
}
