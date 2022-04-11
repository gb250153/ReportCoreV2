using ReportCoreV2.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models
{
    public class ApprovedScenarioModel : IApprovedScenarioModel
    {
        public ApprovedScenarioModel()
        {
            ApprovedScenariosData = new List<ApprovedScenariosFields>();
        }
        public List<ApprovedScenariosFields> ApprovedScenariosData { get; set; }
    }

    public class ApprovedScenariosFields

    {

        public string ProjectOwner { get; set; }
        public string Year { get; set; }
        public Int32 January { get; set; }
        public Int32 February { get; set; }
        public Int32 March { get; set; }
        public Int32 April { get; set; }
        public Int32 May { get; set; }
        public Int32 June { get; set; }
        public Int32 July { get; set; }
        public Int32 August { get; set; }
        public Int32 September { get; set; }
        public Int32 October { get; set; }
        public Int32 November { get; set; }
        public Int32 December { get; set; }
        public Int32 ProjectTotal { get; set; }

    }

   
}
