using ReportCoreV2.Models;
using System.Collections.Generic;

namespace ReportCoreV2.DataRepository
{
    public interface IProjectDashboardData
    {
        IProjectDashboardModel GetCurrentMonthTotalScenarioCreated(string ProjectFilter);
        IProjectDashboardModel GetCurrentMonthTotalScenarioExecution(string ProjectFilter);
        IProjectDashboardModel GetCurrentMonthTotalScenarioMaintain(string ProjectFilter);
        IProjectDashboardModel GetCurrentYearTotalScenarioCreated(string ProjectFilter);
        IProjectDashboardModel GetCurrentYearTotalScenarioExecution(string ProjectFilter);
        IProjectDashboardModel GetCurrentYearTotalScenarioMaintain(string ProjectFilter);
        List<CycleResultFieldsForProjectDashboard> GetCycleDataForProjectDashboard(string ProjectFilter);
        IProjectDashboardModel GetProjectTotalScenarios(string ProjectFilter);
        List<DataListOfProjects> GetProjectsListForDropdown();
        IProjectDashboardModel GetLastYearCurrentMonthTotalScenarioExecution(string ProjectFilter);
        IProjectDashboardModel GetLastYearTotalScenarioExecution(string ProjectFilter);
        IProjectDashboardModel GetLastYearTotalScenarioCreated(string ProjectFilter);
        IProjectDashboardModel GetLastYearCurrentMonthTotalScenarioCreated(string ProjectFilter);
        IProjectDashboardModel GetLastyearCurrentMonthTotalScenarioMaintain(string ProjectFilter);
        IProjectDashboardModel GetLastYearTotalScenarioMaintain(string ProjectFilter);
        IProjectDashboardModel GetProjectCreatedByList(string ProjectFilter);
    }
}