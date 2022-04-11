using ReportCoreV2.DataRepository;
using ReportCoreV2.Models.ModelInterfaces;
using ReportCoreV2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.BusinessDataHandler
{
    public class ExternalDataHandler : IExternalDataHandler
    {
       // private IExternalDataAddViewModel _externalDataAddViewModel;
        private IExternalAddExecution _externalAddExecution;
        private IExternalAddCreatedScenarios _externalAddCreatedScenarios;
        private IExtenalData _extenalData;
        private IExternalExecutionDataModel _externalExecutionDataModel;
        private IExternalExecutionData _externalExecutionData;
        private IExternalDashboardViewModel _externalDashboardViewModel;
        private IExternalApprovedScenarioData _externalApprovedScenarioData;
        private IExternalApprovedScenarioModel _externalApprovedScenarioModel;

        public ExternalDataHandler(IExternalApprovedScenarioModel externalApprovedScenarioModel, IExternalApprovedScenarioData externalApprovedScenarioData, IExternalDashboardViewModel externalDashboardViewModel, IExternalExecutionData externalExecutionData, IExternalExecutionDataModel externalExecutionDataModel, IExternalAddExecution externalAddExecution, IExtenalData extenalData, IExternalAddCreatedScenarios externalAddCreatedScenarios)
        {
           // _externalDataAddViewModel = externalDataAddViewModel;
            _externalAddExecution = externalAddExecution;
            _extenalData = extenalData;
            _externalAddCreatedScenarios = externalAddCreatedScenarios;
            _externalExecutionDataModel = externalExecutionDataModel;
            _externalExecutionData = externalExecutionData;
            _externalDashboardViewModel = externalDashboardViewModel;
            _externalApprovedScenarioData = externalApprovedScenarioData;
            _externalApprovedScenarioModel = externalApprovedScenarioModel;
        }

        public IExternalAddExecution AddExecutionData(IExternalDataAddViewModel externalDataAddViewModel)
        {

            _externalAddExecution.ProjectName = externalDataAddViewModel.Project;
            _externalAddExecution.EntryDate = externalDataAddViewModel.EntryDate;
            _externalAddExecution.ScenarioExecutionAmount = externalDataAddViewModel.Amount;
            _extenalData.AddNewExecution(_externalAddExecution);
            return _externalAddExecution;
        }

        public IExternalAddCreatedScenarios AddCreatedScenariosData(IExternalDataAddViewModel externalDataAddViewModel)
        {

            _externalAddCreatedScenarios.ProjectName = externalDataAddViewModel.Project;
            _externalAddCreatedScenarios.EntryDate = externalDataAddViewModel.EntryDate;
            _externalAddCreatedScenarios.ScenarioCreatedAmount = externalDataAddViewModel.Amount;

            _extenalData.AddNewCreatedScenarios(_externalAddCreatedScenarios);
            return _externalAddCreatedScenarios;
        }

        public IExternalApprovedScenarioModel GetExternalApprovedScenarioData()
        {
            string SelectedYear = DateTime.Today.Year.ToString();
            _externalApprovedScenarioModel.ExternalApprovedScenarioForDashboard = _externalApprovedScenarioData.GetExternalApprovedScenarioForDashboard(SelectedYear);
            return _externalApprovedScenarioModel;
        }
        private IExternalExecutionDataModel GetExternalExecutionLogDataForDashboardChart()
        {
            string SelectedYear = DateTime.Today.Year.ToString();
            _externalExecutionDataModel.ExternalExecutionDataForDashboard = _externalExecutionData.GetExternalExecutionDataForDashboard(SelectedYear);
            

            return _externalExecutionDataModel;

        }

        private IExternalExecutionDataModel GetExternalExecutionByWeekDataForDashboardChart()
        {
            string SelectedYear = DateTime.Today.Year.ToString();
            _externalExecutionDataModel.ExternalExecutionDataForDashboard = _externalExecutionData.GetExternalExecutionByWeekForDashboards(SelectedYear);


            return _externalExecutionDataModel;

        }
        private IExternalApprovedScenarioModel GetCreatedScenarioByWeekDataForDashboardChart()
        {
            
            string SelectedYear = DateTime.Today.Year.ToString();

            _externalApprovedScenarioModel.ExternalApprovedScenariosByWeekDataForDashboard = _externalApprovedScenarioData.GetExternalApprovedScenarioByWeekForDashboards(SelectedYear);


            return _externalApprovedScenarioModel;

        }
        private List<DataPointsForGraphsViewModel> GetExecutionDataForChartsWithNewDatapoints()
        {
            GetExternalExecutionLogDataForDashboardChart();
            var listOfDataPoints = new List<DataPointsForGraphsViewModel>();
            var ProjectId = Guid.NewGuid();
            var PointsValues = new List<DataPoints>();
            string Total;
            var SortedList = _externalExecutionDataModel.ExternalExecutionDataForDashboard.OrderBy(p => p.Project);
            foreach (var item in SortedList)
            {

                // var Year = item.Year;

                PointsValues.Add(new DataPoints() { ColumnLabel = item.Project, ColumnValue = item.ProjectTotal });
            }
            Total = _externalExecutionDataModel.ExternalExecutionDataForDashboard.Sum(x => x.ProjectTotal).ToString();
            listOfDataPoints.Add(new DataPointsForGraphsViewModel() { DataPointsList = PointsValues, GuidID = ProjectId, ProjectTotal = Total });



            return listOfDataPoints;
        }

        private List<DataPointsForGraphsViewModel> GetApprovedDataForChartsWithNewDatapoints()
        {
            GetExternalApprovedScenarioData();
            var listOfDataPoints = new List<DataPointsForGraphsViewModel>();
            var ProjectId = Guid.NewGuid();
            var PointsValues = new List<DataPoints>();
            string Total;
            var SortedList = _externalApprovedScenarioModel.ExternalApprovedScenarioForDashboard.OrderBy(p => p.Project);
            foreach (var item in SortedList)
            {

                // var Year = item.Year;

                PointsValues.Add(new DataPoints() { ColumnLabel = item.Project, ColumnValue = item.ProjectTotal });
            }
            Total = _externalApprovedScenarioModel.ExternalApprovedScenarioForDashboard.Sum(x => x.ProjectTotal).ToString();
            listOfDataPoints.Add(new DataPointsForGraphsViewModel() { DataPointsList = PointsValues, GuidID = ProjectId, ProjectTotal = Total });



            return listOfDataPoints;
        }

        private List<DataPointsForGraphsViewModel> GetApprovedScenarioByWeekDataForChartsWithNewDatapoints()
        {
            GetCreatedScenarioByWeekDataForDashboardChart();
            var listOfDataPoints = new List<DataPointsForGraphsViewModel>();
            var ProjectId = Guid.NewGuid();
            var PointsValues = new List<DataPoints>();

            string Total;
            var SortedList = _externalApprovedScenarioModel.ExternalApprovedScenariosByWeekDataForDashboard.OrderBy(p => p.Project);

            foreach (var item in _externalApprovedScenarioModel.ExternalApprovedScenariosByWeekDataForDashboard)
            {

                PointsValues.Add(new DataPoints() { ColumnLabel = ("Week " + item.NumberOfWeek), ColumnValue = item.ProjectTotal });


                //PointsValues.Add(new DataPoints() { ColumnLabel = item.DateOfTotal.Date.ToString("dd/MM/yyyy"), ColumnValue = item.ProjectTotal });
            }

            var AggregatePointsValue = PointsValues.GroupBy(c => c.ColumnLabel).Select(x => new DataPoints { ColumnLabel = x.Key, ColumnValue = x.Sum(s => s.ColumnValue) });

            Total = _externalApprovedScenarioModel.ExternalApprovedScenariosByWeekDataForDashboard.Sum(x => x.ProjectTotal).ToString();
            listOfDataPoints.Add(new DataPointsForGraphsViewModel() { DataPointsList = AggregatePointsValue.ToList(), GuidID = ProjectId, ProjectTotal = Total });



            return listOfDataPoints;
        }

        private List<DataPointsForGraphsViewModel> GetExecutionByWeekDataForChartsWithNewDatapoints()
        {
            GetExternalExecutionByWeekDataForDashboardChart();
            var listOfDataPoints = new List<DataPointsForGraphsViewModel>();
            var ProjectId = Guid.NewGuid();
            var PointsValues = new List<DataPoints>();

            string Total;
            var SortedList = _externalExecutionDataModel.ExternalExecutionByWeekDataForDashboard.OrderBy(p => p.Project);

            foreach (var item in _externalExecutionDataModel.ExternalExecutionByWeekDataForDashboard)
            {

                PointsValues.Add(new DataPoints() { ColumnLabel = ("Week " + item.NumberOfWeek), ColumnValue = item.ProjectTotal });


                //PointsValues.Add(new DataPoints() { ColumnLabel = item.DateOfTotal.Date.ToString("dd/MM/yyyy"), ColumnValue = item.ProjectTotal });
            }

            var AggregatePointsValue = PointsValues.GroupBy(c => c.ColumnLabel).Select(x => new DataPoints { ColumnLabel = x.Key, ColumnValue = x.Sum(s => s.ColumnValue) });

            Total = _externalExecutionDataModel.ExternalExecutionByWeekDataForDashboard.Sum(x => x.ProjectTotal).ToString();
            listOfDataPoints.Add(new DataPointsForGraphsViewModel() { DataPointsList = AggregatePointsValue.ToList(), GuidID = ProjectId, ProjectTotal = Total });



            return listOfDataPoints;
        }
        private IExternalDashboardViewModel MapModelToView()
        {
            _externalDashboardViewModel.ExternalExecutionCurrentYearDashboardDataForGraphs = GetExecutionDataForChartsWithNewDatapoints();
            _externalDashboardViewModel.ExternalCreationCurrentYearDashboardDataForGraphs = GetApprovedDataForChartsWithNewDatapoints();
            _externalDashboardViewModel.ExternalCreationByWeekDashboardDataForGraphs = GetApprovedScenarioByWeekDataForChartsWithNewDatapoints();
            _externalDashboardViewModel.ExternalExecutionByWeekDashboardDataForGraphs = GetExecutionByWeekDataForChartsWithNewDatapoints();
            _externalDashboardViewModel.YearofData = DateTime.Today.Year.ToString(); ;
            _externalDashboardViewModel.LastYearofData = DateTime.Today.AddYears(-1).Year.ToString();
            _externalDashboardViewModel.MonthName = DateTime.Today.ToString("MMMM");
            return _externalDashboardViewModel;
        }

        public IExternalDashboardViewModel GetExternalDashboard()
        {
            MapModelToView();
            return _externalDashboardViewModel;
        }
    }
}
