using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models.ViewModel
{
    public class DurationListViewModel : IDurationListViewModel
    {
        public List<Durationinfo> DurationDataForUi { get; set; }
    }
}
