using ReportCoreV2.Models;
using System.Collections.Generic;

namespace ReportCoreV2.DataRepository
{
    public interface IExternalApprovedScenarioData
    {
        List<ExternalCreatedScenarioForDashboard> GetExternalApprovedScenarioByWeekForDashboards(string YearFilter);
        List<ExternalApprovedDataFields> GetExternalApprovedScenarioData(string YearFilter, string ProjectFilter);
        List<ExternalCreatedScenarioForDashboard> GetExternalApprovedScenarioForDashboard(string YearFilter);
        List<ExternalCreatedScenarioForDashboard> GetExternalLastYearApprovedScenarioForDashboard(string YearFilter);
    }
}