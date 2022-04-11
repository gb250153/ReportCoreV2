using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ReportCoreV2.Models.ViewModel
{
    public class ProjectDashboardViewModel : IProjectDashboardViewModel
    {
        List<DataListOfProjects> _projectsData = new List<DataListOfProjects>();
        public List<DataPointsForGraphsViewModel> LastYearExecutionLogCurrentmonthDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> ExecutionLogCurrentmonthDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> ApprovedScenarioLogProjectDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> ApprovedScenarioLogByWeekDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> LastYearApprovedScenarioLogByWeekDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> ExecutionLogProjectDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> CycleDataProjectDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> ScenarioTypeProjectDashboardDataForGraphs { get; set; }
        public List<DataPointsForGraphsViewModel> ProjectAppLinksDashboardDataForGraphs { get; set; }
        public List<ProjectAppLinkInfo> ProjectAppLinks { get; set; }
        public List<ProjectScenarioCreatedBy> ProjectUsersList { get; set; }

        public SelectList ProjectsDatalistAsString
        {
            get
            {
                var listToCOnvert = new List<string>();

                foreach (var item in _projectsData)
                {
                    listToCOnvert.Add(item.Project.ToString());
                }

                return new SelectList(listToCOnvert);
            }
        }
        public List<DataListOfProjects> ProjectsData { get { return _projectsData; } set { _projectsData = value; } }
        public string selectedproject { get; set; }
        public string ProjectName { get; set; }
        public string YearofData { get; set; }
        public string LastYearofData { get; set; }
        public string MonthName { get; set; }
        public string TotalOfCurrentYearExecution { get; set; }
        public string TotalOfCurrentMonthExecution { get; set; }
        public string TotalOfCurrentYearCreated { get; set; }
        public string TotalOfCurrentMonthCreated { get; set; }
        public string TotalOfCurrentYearMaintain { get; set; }
        public string TotalOfCurrentMonthMaintain { get; set; }
        public string NoDataMessage { get; set; }
        public string TotalScenariosInProject { get; set; }
        public string ScenarioTypeRegression { get; set; }
        public string ScenarioTypeSanity { get; set; }
        public string TotalOfLastYearExecution { get; set; }
        public string TotalOfLastYearCurrentMonthExecution { get; set; }

        public string TotalOfLastYearCreated { get; set; }
        public string TotalOfLastYearCurrentMonthCreated { get; set; }

        public string TotalOfLastYearMaintain { get; set; }
        public string TotalOfLastYearCurrentMonthMaintain { get; set; }

    }
}
