using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models
{
    public class MonthlyDataAggregate
    {
        public Nullable<int> Sum_January { get; set; } = 0;
        public Nullable<int> Sum_February { get; set; } = 0;
        public Nullable<int> Sum_March { get; set; } = 0;
        public Nullable<int> Sum_April { get; set; } = 0;
        public Nullable<int> Sum_May { get; set; } = 0;
        public Nullable<int> Sum_June { get; set; } = 0;
        public Nullable<int> Sum_July { get; set; } = 0;
        public Nullable<int> Sum_August { get; set; } = 0;
        public Nullable<int> Sum_September { get; set; } = 0;
        public Nullable<int> Sum_October { get; set; } = 0;
        public Nullable<int> Sum_November { get; set; } = 0;
        public Nullable<int> Sum_December { get; set; } = 0;
    }
}
