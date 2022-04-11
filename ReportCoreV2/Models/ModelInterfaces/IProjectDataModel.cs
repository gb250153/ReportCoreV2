using System.Collections.Generic;

namespace ReportCoreV2.Models.ModelInterfaces
{
    public interface IProjectDataModel
    {
        List<DataListOfProjects> ProjectList { get; set; }
    }
}