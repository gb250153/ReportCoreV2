using ReportCoreV2.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models
{
    public class DurationInfoModel : IDurationInfoModel
    {
        public DurationInfoModel()
        {
            Duration = new List<Durationinfo>();
        }
        public List<Durationinfo> Duration { get; set; }
    }

    public class Durationinfo

    {
        public string Host { get; set; }
        public string SetName { get; set; }
        public string SetContent { get; set; }
        public string SetTestRun { get; set; }
        public string SetDuration { get; set; }
        public string SetTimeDate { get; set; }
        public string ScenarioName { get; set; }
        public string ScenarioTimeDate { get; set; }
        public string ScenarioNumber { get; set; }
        public string ScenarioDuration { get; set; }
        public string ActionNumber { get; set; }
        public string ActionName { get; set; }
        public string ActionActual { get; set; }
        public string ActionDuration { get; set; }
        public string ActionTimeDate { get; set; }


    }
}
