using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models.ViewModel
{
    public class ApprovedScenarioViewModel : IApprovedScenarioViewModel
    {
        public List<ApprovedScenariosFields> ApprovedScenarioDataForUi { get; set; }
    }
}
