using System.Collections.Generic;

namespace ReportCoreV2.Models.ViewModel
{
    public interface IApprovedScenarioViewModel
    {
        List<ApprovedScenariosFields> ApprovedScenarioDataForUi { get; set; }
    }
}