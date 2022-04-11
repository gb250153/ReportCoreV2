using System;
using System.Collections.Generic;

#nullable disable

namespace ReportCoreV2.ExternalEfModel
{
    public partial class ManualScenarioCreation
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ScenarioCreatedAmount { get; set; }
    }
}
