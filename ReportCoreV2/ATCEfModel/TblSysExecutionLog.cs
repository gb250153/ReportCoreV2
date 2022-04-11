using System;
using System.Collections.Generic;

#nullable disable

namespace ReportCoreV2.ATCEfModel
{
    public partial class TblSysExecutionLog
    {
        public string Project { get; set; }
        public DateTime DateReq { get; set; }
        public string TestSetName { get; set; }
        public string RunOnHost { get; set; }
        public string Status { get; set; }
        public string Process { get; set; }
        public long RequestNum { get; set; }
        public string RequestBy { get; set; }
        public string IsSchedule { get; set; }
        public DateTime ScheduleSetTime { get; set; }
        public long DependenceReq { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public string SendMailTo { get; set; }
        public string SendMailOnStart { get; set; }
        public string ResultXmlfile { get; set; }
        public long? TotalDurationTime { get; set; }
        public int? ScenarioCounter { get; set; }
        public int? ScenarioCounterExecuted { get; set; }
        public double? ScenarioAvgDurationTime { get; set; }
        public int? ActionCounter { get; set; }
        public int? ActionCounterExecuted { get; set; }
        public double? ActionAvgDurationTime { get; set; }
        public string TestSetCycle { get; set; }
        public string TimeStamp { get; set; }
        public int ExecutionModeId { get; set; }
        public int? ScenarioCounterExecutedPass { get; set; }
        public int? ScenarioCounterExecutedFail { get; set; }
        public int? ScenarioCounterExecutedTError { get; set; }
        public int? ScenarioCounterExecutedFError { get; set; }
        public int? Env { get; set; }
        public string TestSetExecutionId { get; set; }
        public int? ActionCounterExecutedPass { get; set; }
        public int? ActionCounterExecutedFail { get; set; }
        public int? ActionCounterExecutedTError { get; set; }
        public int? ActionCounterExecutedFError { get; set; }
        public string AppType { get; set; }
    }
}
