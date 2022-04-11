using System.Collections.Generic;

namespace ReportCoreV2.Models.ModelInterfaces
{
    public interface IExternalProjectModel
    {
        List<DataListOfExternalProjects> ExternalProjectList { get; set; }
    }
}