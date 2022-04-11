using System.Collections.Generic;

namespace ReportCoreV2.Models.ModelInterfaces
{
    public interface IDashboardModel
    {
        List<ExecutionLogFieldsForDashboard> ApprovedScenariosByWeekDataForDashboard { get; set; }
        List<ExecutionLogFieldsForDashboard> ApprovedScenariosDataForDashboard { get; set; }
        List<ExecutionLogFieldsForDashboard> CurrentMonthExecutionLogDataForDashboard { get; set; }
        List<ExecutionLogFieldsForDashboard> ExecutionLogDataForDashboard { get; set; }
        List<ExecutionLogFieldsForDashboard> ApprovedLastyearScenariosDataForDashboard { get; set; }
        List<ExecutionLogFieldsForDashboard> LastYearExecutionLogDataForDashboard { get; set; }
    }
}