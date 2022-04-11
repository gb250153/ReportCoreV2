using ReportCoreV2.Models;
using System.Collections.Generic;

namespace ReportCoreV2.DataRepository
{
    public interface IDurationInfoData
    {
        List<Durationinfo> GetDurationDataForGrid();
    }
}