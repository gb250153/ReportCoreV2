using System.Collections.Generic;

namespace ReportCoreV2.Models.ModelInterfaces
{
    public interface IApprovedScenarioModel
    {
        List<ApprovedScenariosFields> ApprovedScenariosData { get; set; }
    }
}