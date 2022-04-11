using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models.ViewModel
{
    public class ExecutionLogViewModel : IExecutionLogViewModel
    {
        public List<ExecutionLogFields> ExecutionLogDataForUi { get; set; }
        public List<ExecutionLogFieldsForExcel> ExecutionLogDataForExport { get; set; }
    }
}
