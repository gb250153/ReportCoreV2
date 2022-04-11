using System.Collections.Generic;

namespace ReportCoreV2.Models.ModelInterfaces
{
    public interface IExternalApprovedScenarioModel
    {
        List<ExternalApprovedDataFields> ExternalApprovedScenarioData { get; set; }
        List<ExternalCreatedScenarioForDashboard> ExternalApprovedScenarioForDashboard { get; set; }
        List<ExternalCreatedScenarioForDashboard> ExternalLastYearApprovedScenarioForDashboard { get; set; }
        List<ExternalCreatedScenarioForDashboard> ExternalApprovedScenariosByWeekDataForDashboard { get; set; }
    }
}