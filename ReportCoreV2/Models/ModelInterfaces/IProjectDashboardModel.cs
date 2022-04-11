using System.Collections.Generic;

namespace ReportCoreV2.Models
{
    public interface IProjectDashboardModel
    {
        List<ExecutionLogFieldsForProjectDashboard> ApprovedScenariosByWeekDataForDashboard { get; set; }
        List<ExecutionLogFieldsForProjectDashboard> ApprovedScenariosDataForProjectDashboard { get; set; }
        List<ExecutionLogFieldsForProjectDashboard> CurrentMonthExecutionLogDataForDashboard { get; set; }
        List<CycleResultFieldsForProjectDashboard> CycleResultDataForProjectDashboard { get; set; }
        List<ExecutionLogFieldsForProjectDashboard> ExecutionLogDataForProjectDashboard { get; set; }
        string TotalExecutedScenarioInCurrentMonth { get; set; }
        string TotalExecutedScenarioInCurrentYear { get; set; }
        string TotalCreatedScenarioInCurrentYear { get; set; }
        string TotalCreatedScenarioInCurrentMonth { get; set; }
        string TotalMaintainScenarioInCurrentMonth { get; set; }
        string TotalMaintainScenarioInCurrentYear { get; set; }
        string NoDataMessage { get; set; }
        List<ProjectAppLinkInfo> projectAppLinks { get; set; }
        List<ProjectScenarioTypeInfo> projectScenarioTypes { get; set; }
        string TotalScenariosInProject { get; set; }
        string ScenarioTypeRegression { get; set; }
        string ScenarioTypeSanity { get; set; }
        string TotalLastYearExecutedScenarioInCurrentMonth { get; set; }
        string TotalExecutedScenarioInLastYear { get; set; }
        string TotalCreatedScenarioInLastYear { get; set; }
        string TotalCreatedScenarioInLastYearCurrentMonth { get; set; }
        string TotalMaintainScenarioInLastYearCurrentMonth { get; set; }
        string TotalMaintainScenarioInLastYear { get; set; }
        List<ProjectScenarioCreatedBy> ProjectScenarioCreatedBy { get; set; }
    }
}