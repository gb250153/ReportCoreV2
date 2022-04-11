using System;

namespace ReportCoreV2.Models.ModelInterfaces
{
    public interface IExternalAddCreatedScenarios
    {
        DateTime EntryDate { get; set; }
        string ProjectName { get; set; }
        int? ScenarioCreatedAmount { get; set; }
    }
}