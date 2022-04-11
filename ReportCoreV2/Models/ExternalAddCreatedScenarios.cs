using ReportCoreV2.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models
{
    public class ExternalAddCreatedScenarios : IExternalAddCreatedScenarios
    {
        public string ProjectName { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ScenarioCreatedAmount { get; set; }
    }
}
