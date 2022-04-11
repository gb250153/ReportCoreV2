using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ReportCoreV2.ExternalEfModel
{
    public partial class ManualExecution
    {
       
        public int Id { get; set; }
        
        public string ProjectName { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ScenarioExecutionAmount { get; set; }
    }
}
