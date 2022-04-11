using System;
using System.Collections.Generic;

#nullable disable

namespace ReportCoreV2.ATCEfModel
{
    public partial class TblSysCycleDuration
    {
        public long Pk { get; set; }
        public string TestCycle { get; set; }
        public string TestCycleTimeStamp { get; set; }
        public string TestSet { get; set; }
        public string TestSetExecutionId { get; set; }
        public string Scenario { get; set; }
        public string Action { get; set; }
        public decimal ActionDuration { get; set; }
        public string ActionStatus { get; set; }
        public string ActionFailReason { get; set; }
    }
}
