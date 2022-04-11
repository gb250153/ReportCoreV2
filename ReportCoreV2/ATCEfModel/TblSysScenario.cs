using System;
using System.Collections.Generic;

#nullable disable

namespace ReportCoreV2.ATCEfModel
{
    public partial class TblSysScenario
    {
        public int Id { get; set; }
        public string ProjectOwner { get; set; }
        public string ScenarioName { get; set; }
        public string ScenarioDesc { get; set; }
        public DateTime? ScenarioCreationDate { get; set; }
        public DateTime? ScenarioLastUpdate { get; set; }
        public string ScenarioTableName { get; set; }
        public string ScenarioCreateBy { get; set; }
        public string ScenarioComment { get; set; }
        public string ScenarioStatus { get; set; }
        public string AbortOnError { get; set; }
        public string FsdVersion { get; set; }
        public string ScenarioType { get; set; }
        public string Product { get; set; }
        public string RecordMovie { get; set; }
        public string Ddt { get; set; }
        public int? CheckTicketbalance { get; set; }
        public string RootFolder { get; set; }
        public string ParentRootFolder { get; set; }
        public string ScenarioPath { get; set; }
        public string AppLink { get; set; }
        public string MarketPlace { get; set; }
        public string ScenarioTemplateInstanceKeyWord { get; set; }
        public string ScenarioTemplateLink { get; set; }
        public string ScenarioTemplatePath { get; set; }
        public string ScenarioCustomerName { get; set; }
        public string Lock { get; set; }
        public string Ler { get; set; }
        public string Led { get; set; }
        public string Les { get; set; }
        public Guid? Guid { get; set; }
        public bool IsHighlight { get; set; }
        public bool IsPerformance { get; set; }
        public int? ScenarioActionCount { get; set; }
        public int? QcId { get; set; }
    }
}
