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
    public class ApprovedScenarioDataHandler : IApprovedScenarioDataHandler
    {
        private IApprovedScenarioData _approvedScenarioData;
        private IApprovedScenarioViewModel _approvedScenarioViewModel;

        public ApprovedScenarioDataHandler(IApprovedScenarioData approvedScenarioData, IApprovedScenarioViewModel approvedScenarioViewModel)
        {
            _approvedScenarioData = approvedScenarioData;
            _approvedScenarioViewModel = approvedScenarioViewModel;
        }

        public IApprovedScenarioViewModel GetDataAndTotalsForGrid()
        {
            string State = "Approved";
            string selectedyear = "";
            string selectedproject = "";
            _approvedScenarioViewModel.ApprovedScenarioDataForUi = _approvedScenarioData.GetApprovedScenariosDataForGrid(State, selectedyear, selectedproject);

            return _approvedScenarioViewModel;

        }


        public DataTable ConvertToDataTable(List<ApprovedScenariosFields> approvedScenarioForExcel)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ApprovedScenariosFields));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (ApprovedScenariosFields item in approvedScenarioForExcel)
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
