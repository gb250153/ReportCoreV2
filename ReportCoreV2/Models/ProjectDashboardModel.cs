using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models
{
    public class ProjectDashboardModel : IProjectDashboardModel
    {
        public ProjectDashboardModel()
        {
            ExecutionLogDataForProjectDashboard = new List<ExecutionLogFieldsForProjectDashboard>();
            CurrentMonthExecutionLogDataForDashboard = new List<ExecutionLogFieldsForProjectDashboard>();
            ApprovedScenariosDataForProjectDashboard = new List<ExecutionLogFieldsForProjectDashboard>();
            ApprovedScenariosByWeekDataForDashboard = new List<ExecutionLogFieldsForProjectDashboard>();
            CycleResultDataForProjectDashboard = new List<CycleResultFieldsForProjectDashboard>();
            projectAppLinks = new List<ProjectAppLinkInfo>();
            projectScenarioTypes = new List<ProjectScenarioTypeInfo>();
            ProjectScenarioCreatedBy = new List<ProjectScenarioCreatedBy>();
        }
        public List<ExecutionLogFieldsForProjectDashboard> ExecutionLogDataForProjectDashboard { get; set; }
        public List<ExecutionLogFieldsForProjectDashboard> CurrentMonthExecutionLogDataForDashboard { get; set; }
        public List<ExecutionLogFieldsForProjectDashboard> ApprovedScenariosDataForProjectDashboard { get; set; }
        public List<ExecutionLogFieldsForProjectDashboard> ApprovedScenariosByWeekDataForDashboard { get; set; }
        public List<CycleResultFieldsForProjectDashboard> CycleResultDataForProjectDashboard { get; set; }
        public List<ProjectAppLinkInfo> projectAppLinks { get; set; }
        public List<ProjectScenarioTypeInfo> projectScenarioTypes { get; set; }

        public List<ProjectScenarioCreatedBy> ProjectScenarioCreatedBy { get; set; }
        public string TotalExecutedScenarioInCurrentMonth { get; set; }
        public string TotalExecutedScenarioInCurrentYear { get; set; }
        public string TotalCreatedScenarioInCurrentYear { get; set; }
        public string TotalCreatedScenarioInCurrentMonth { get; set; }
        public string TotalMaintainScenarioInCurrentMonth { get; set; }
        public string TotalMaintainScenarioInCurrentYear { get; set; }
        public string NoDataMessage { get; set; }
        public string TotalScenariosInProject { get; set; }
        public string ScenarioTypeRegression { get; set; }
        public string ScenarioTypeSanity { get; set; }
        public string TotalLastYearExecutedScenarioInCurrentMonth { get; set; }
        public string TotalExecutedScenarioInLastYear { get; set; }
        public string TotalCreatedScenarioInLastYear { get; set; }
        public string TotalCreatedScenarioInLastYearCurrentMonth { get; set; }
        public string TotalMaintainScenarioInLastYearCurrentMonth { get; set; }
        public string TotalMaintainScenarioInLastYear { get; set; }
    }


  public class ExecutionLogFieldsForProjectDashboard

    {
        public Guid Projectid { get; set; }
        public string Project { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public Int32 ProjectTotal { get; set; }
        public DateTime DateOfTotal { get; set; }
        public string NumberOfWeek { get; set; }
        public Int32 January { get; set; }
        public Int32 February { get; set; }
        public Int32 March { get; set; }
        public Int32 April { get; set; }
        public Int32 May { get; set; }
        public Int32 June { get; set; }
        public Int32 July { get; set; }
        public Int32 August { get; set; }
        public Int32 September { get; set; }
        public Int32 October { get; set; }
        public Int32 November { get; set; }
        public Int32 December { get; set; }
    }

    public class CycleResultFieldsForProjectDashboard
    {
        public Guid Cycleid { get; set; }
        public string Project { get; set; }
        public string CycleName { get; set; }
        public string Timestemp { get; set; }
        public Int32 SetCountInCycle { get; set; }
        public Int32 ScenarioCountInCycle { get; set; }
        public Int32 ScenarioExecutedInCycle { get; set; }
        public Int32 PassScenario { get; set; }
        public Int32 FailScenario { get; set; }
        public Int32 FlowErrorScenario { get; set; }
        public Int32 TechErrorScenario { get; set; }
        public DateTime LatestDate { get; set; }

    }
    public class ProjectAppLinkInfo
    {
        public string Name { get; set; }
        public string Amount { get; set; }
       


    }
    public class ProjectScenarioTypeInfo
    {
        public string  Regression { get; set; }
        public string Sanity { get; set; }
    }

    public class ProjectScenarioCreatedBy
    {
        public string Name { get; set; }
        public string AmountCreated { get; set; }
        public DateTime LastCreated { get; set; }

    }
}

