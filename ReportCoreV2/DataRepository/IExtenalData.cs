using ReportCoreV2.Models;
using ReportCoreV2.Models.ModelInterfaces;
using System.Collections.Generic;

namespace ReportCoreV2.DataRepository
{
    public interface IExtenalData
    {
        void AddNewCreatedScenarios(IExternalAddCreatedScenarios externalAddCreatedScenarios);
        void AddNewExecution(IExternalAddExecution externalAdd);
        List<DataListOfExternalProjects> GetExternalProjectList();
    }
}