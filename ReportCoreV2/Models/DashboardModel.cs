using ReportCoreV2.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models
{
    public class DashboardModel : IDashboardModel
    {
        public DashboardModel()
        {
            LastYearExecutionLogDataForDashboard = new List<ExecutionLogFieldsForDashboard>();
            ExecutionLogDataForDashboard = new List<ExecutionLogFieldsForDashboard>();
            CurrentMonthExecutionLogDataForDashboard = new List<ExecutionLogFieldsForDashboard>();
            ApprovedScenariosDataForDashboard = new List<ExecutionLogFieldsForDashboard>();
            ApprovedScenariosByWeekDataForDashboard = new List<ExecutionLogFieldsForDashboard>();
            ApprovedLastyearScenariosDataForDashboard = new List<ExecutionLogFieldsForDashboard>();
        }
        public List<ExecutionLogFieldsForDashboard> ExecutionLogDataForDashboard { get; set; }
        public List<ExecutionLogFieldsForDashboard> LastYearExecutionLogDataForDashboard { get; set; }
        public List<ExecutionLogFieldsForDashboard> CurrentMonthExecutionLogDataForDashboard { get; set; }
        public List<ExecutionLogFieldsForDashboard> ApprovedScenariosDataForDashboard { get; set; }
        public List<ExecutionLogFieldsForDashboard> ApprovedLastyearScenariosDataForDashboard { get; set; }
        public List<ExecutionLogFieldsForDashboard> ApprovedScenariosByWeekDataForDashboard { get; set; }
    }

    public class ExecutionLogFieldsForDashboard

    {
        public Guid Projectid { get; set; }
        public string Project { get; set; }
        public string Year { get; set; }
        public Int32 ProjectTotal { get; set; }
        public DateTime DateOfTotal { get; set; }
        public string NumberOfWeek { get; set; }
    }
}
