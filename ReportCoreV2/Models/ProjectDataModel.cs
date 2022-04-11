using ReportCoreV2.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models
{
    public class ProjectDataModel : IProjectDataModel
    {
        public ProjectDataModel()
        {

            ProjectList = new List<DataListOfProjects>();
        }
        public List<DataListOfProjects> ProjectList { get; set; }



    }
    public class DataListOfProjects
    {
        public string Project { get; set; }
    }
}
