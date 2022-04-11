using ReportCoreV2.Models.ViewModel;

namespace ReportCoreV2.BusinessDataHandler
{
    public interface IProjectDashboardDataHandler
    {
        IProjectDashboardViewModel GetProjectList();
       
        IProjectDashboardViewModel MapToView(string ProjectFilter);
    }
}