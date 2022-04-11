using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReportCoreV2.DataRepository;
using ReportCoreV2.Models;
using ReportCoreV2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.BusinessDataHandler
{
    public class ExecutionDataHandler : IExecutionDataHandler
    {
        
        private IExecutionData _executionData;

        private IExecutionLogViewModel _executionLogViewModel;

        public ExecutionDataHandler(IExecutionData executionData, IExecutionLogViewModel executionLogViewModel)
        {
           
            _executionData = executionData;
            _executionLogViewModel = executionLogViewModel;
        }

        public IExecutionLogViewModel GetDataAndTotalsForGrid()
        {
            string Process = "Completed";
            string selectedyear = "";
            string selectedproject = "";
            _executionLogViewModel.ExecutionLogDataForUi = _executionData.GetExecutionDataForGrid(Process, selectedyear, selectedproject);

            return _executionLogViewModel;

        }
        public IExecutionLogViewModel GetDataAndTotalsForGridForExport()
        {
            string Process = "Completed";
            string selectedyear = "";
            string selectedproject = "";
            _executionLogViewModel.ExecutionLogDataForExport = _executionData.GetExecutionDataForExport(Process, selectedyear, selectedproject);

            

            return _executionLogViewModel;

        }

        public DataTable ConvertToDataTable(List<ExecutionLogFieldsForExcel> executionLogDataForExcel)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ExecutionLogFieldsForExcel));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (ExecutionLogFieldsForExcel item in executionLogDataForExcel)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;

        }


    }
}
