using ReportCoreV2.Models;
using ReportCoreV2.Models.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace ReportCoreV2.BusinessDataHandler
{
    public interface IDurationInfoDataHandler
    {
        DataTable ConvertToDataTable(List<Durationinfo> DurationInfoForExcel);
        IDurationListViewModel GetDurationDataForUi();
    }
}