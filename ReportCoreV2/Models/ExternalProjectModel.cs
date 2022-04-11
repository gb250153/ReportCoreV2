using ReportCoreV2.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models
{
    public class ExternalProjectModel : IExternalProjectModel
    {
        public ExternalProjectModel()
        {
            ExternalProjectList = new List<DataListOfExternalProjects>();
        }
        public List<DataListOfExternalProjects> ExternalProjectList { get; set; }
    }

    public class DataListOfExternalProjects
    {
        public string ProjectName { get; set; }
    }
}
