using ReportCoreV2.Models;
using System.Collections.Generic;

namespace ReportCoreV2.DataRepository
{
    public interface IApprovedScenarioData
    {
        List<ApprovedScenariosFields> GetApprovedScenariosDataForGrid(string StateFilter, string YearFilter, string ProjectFilter);
    }
}