using System;
using System.Collections.Generic;

#nullable disable

namespace ReportCoreV2.ASGEfModel
{
    public partial class AsggeneralInfo
    {
        public string NumberOfLogs { get; set; }
        public string ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ProcessRate { get; set; }
    }
}
