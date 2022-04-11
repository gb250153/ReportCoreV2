using ReportCoreV2.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models
{
    public class ExternalExecutionDataModel : IExternalExecutionDataModel
    {
        public ExternalExecutionDataModel()
        {
            ExternalExecutionData = new List<ExternalExecutionDataFields>();
            ExternalExecutionDataForDashboard = new List<ExternalExecutionForDashboard>();
            ExternalLastYearExecutionDataForDashboard = new List<ExternalExecutionForDashboard>();
            ExternalCurrentMonthExecutionDataForDashboard = new List<ExternalExecutionForDashboard>();
            ExternalExecutionDataForTableUpdate = new List<ExternalExecutionDataFields>();
            ExternalExecutionByWeekDataForDashboard = new List<ExternalExecutionForDashboard>();
        }
        public List<ExternalExecutionDataFields> ExternalExecutionData { get; set; }
        public List<ExternalExecutionForDashboard> ExternalExecutionDataForDashboard { get; set; }
        public List<ExternalExecutionForDashboard> ExternalLastYearExecutionDataForDashboard { get; set; }
        public List<ExternalExecutionForDashboard> ExternalCurrentMonthExecutionDataForDashboard { get; set; }
        public List<ExternalExecutionDataFields> ExternalExecutionDataForTableUpdate { get; set; }
        public List<ExternalExecutionForDashboard> ExternalExecutionByWeekDataForDashboard { get; set; }

    }

    public class ExternalExecutionDataFields
    {
        public string Project { get; set; }
        public string Year { get; set; }
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

        public Int32 ProjectTotal { get; set; }


    }

    public class ExternalExecutionForDashboard

    {
        public Guid Projectid { get; set; }
        public string Project { get; set; }
        public string Year { get; set; }
        public Int32 ProjectTotal { get; set; }
        public DateTime DateOfTotal { get; set; }
        public string NumberOfWeek { get; set; }
    }
}
