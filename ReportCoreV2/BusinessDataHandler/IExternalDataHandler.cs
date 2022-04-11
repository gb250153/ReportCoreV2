using ReportCoreV2.Models.ModelInterfaces;
using ReportCoreV2.Models.ViewModel;

namespace ReportCoreV2.BusinessDataHandler
{
    public interface IExternalDataHandler
    {
        IExternalAddCreatedScenarios AddCreatedScenariosData(IExternalDataAddViewModel externalDataAddViewModel);
        IExternalAddExecution AddExecutionData(IExternalDataAddViewModel externalDataAddViewModel);
        IExternalDashboardViewModel GetExternalDashboard();
    }
}