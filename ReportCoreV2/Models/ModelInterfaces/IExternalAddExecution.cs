using System;

namespace ReportCoreV2.Models.ModelInterfaces
{

    public interface IExternalAddExecution
    {
        DateTime EntryDate { get; set; }
        string ProjectName { get; set; }
        int? ScenarioExecutionAmount { get; set; }
    }
}