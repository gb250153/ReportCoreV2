using ReportCoreV2.Models.ViewModel;

namespace ReportCoreV2.BusinessDataHandler
{
    public interface IExternalProjectDataHandler
    {
        IExternalDataAddViewModel GetExternalProjectList();
    }
}