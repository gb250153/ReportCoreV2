using System.Collections.Generic;

namespace ReportCoreV2.Models.ModelInterfaces
{
    public interface IExternalExecutionDataModel
    {
        List<ExternalExecutionDataFields> ExternalExecutionData { get; set; }
        List<ExternalExecutionForDashboard> ExternalExecutionDataForDashboard { get; set; }
        List<ExternalExecutionForDashboard> ExternalLastYearExecutionDataForDashboard { get; set; }
        List<ExternalExecutionForDashboard> ExternalCurrentMonthExecutionDataForDashboard { get; set; }
        List<ExternalExecutionDataFields> ExternalExecutionDataForTableUpdate { get; set; }
        List<ExternalExecutionForDashboard> ExternalExecutionByWeekDataForDashboard { get; set; }
    }
}