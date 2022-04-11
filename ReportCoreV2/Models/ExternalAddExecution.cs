using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models.ModelInterfaces
{
    public class ExternalAddExecution : IExternalAddExecution
    {
        
        public string ProjectName { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ScenarioExecutionAmount { get; set; }
    }
}
