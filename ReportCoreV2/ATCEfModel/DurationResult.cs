using System;
using System.Collections.Generic;

#nullable disable

namespace ReportCoreV2.ATCEfModel
{
    public partial class DurationResult
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public string Setname { get; set; }
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
