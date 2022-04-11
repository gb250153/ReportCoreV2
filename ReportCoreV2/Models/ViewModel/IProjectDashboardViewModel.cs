using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ReportCoreV2.Models.ViewModel
{
    public interface IProjectDashboardViewModel
    {
        List<DataPointsForGraphsViewModel> ApprovedScenarioLogByWeekDashboardDataForGraphs { get; set; }
        List<DataPointsForGraphsViewModel> ApprovedScenarioLogProjectDashboardDataForGraphs { get; set; }
        List<DataPointsForGraphsViewModel> CycleDataProjectDashboardDataForGraphs { get; set; }
        List<DataPointsForGraphsViewModel> ExecutionLogCurrentmonthDashboardDataForGraphs { get; set; }
        List<DataPointsForGraphsViewModel> ExecutionLogProjectDashboardDataForGraphs { get; set; }
        List<DataPointsForGraphsViewModel> LastYearApprovedScenarioLogByWeekDashboardDataForGraphs { get; set; }
        List<DataPointsForGraphsViewModel> LastYearExecutionLogCurrentmonthDashboardDataForGraphs { get; set; }
        string LastYearofData { get; set; }
        List<DataListOfProjects> ProjectsData { get; set; }
        SelectList ProjectsDatalistAsString { get; }
        string selectedproject { get; set; }
        string YearofData { get; set; }
        string TotalOfCurrentMonthExecution { get; set; }
        string TotalOfCurrentYearExecution { get; set; }
        string MonthName { get; set; }
        string TotalOfCurrentYearCreated { get; set; }
        string TotalOfCurrentMonthCreated { get; set; }
        string TotalOfCurrentYearMaintain { get; set; }
        string TotalOfCurrentMonthMaintain { get; set; }
        string ProjectName { get; set; }
        string NoDataMessage { get; set; }
        List<ProjectAppLinkInfo> ProjectAppLinks { get; set; }
        string TotalScenariosInProject { get; set; }
        string ScenarioTypeRegression { get; set; }
        string ScenarioTypeSanity { get; set; }
        string TotalOfLastYearExecution { get; set; }
        string TotalOfLastYearCurrentMonthExecution { get; set; }
        string TotalOfLastYearCreated { get; set; }
        string TotalOfLastYearCurrentMonthCreated { get; set; }
        string TotalOfLastYearMaintain { get; set; }
        string TotalOfLastYearCurrentMonthMaintain { get; set; }
        List<DataPointsForGraphsViewModel> ScenarioTypeProjectDashboardDataForGraphs { get; set; }
        List<ProjectScenarioCreatedBy> ProjectUsersList { get; set; }
        List<DataPointsForGraphsViewModel> ProjectAppLinksDashboardDataForGraphs { get; set; }
    }
}