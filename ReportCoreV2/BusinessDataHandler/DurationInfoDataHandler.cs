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
    public class DurationInfoDataHandler : IDurationInfoDataHandler
    {
        private IDurationInfoData _durationInfoData;
        private IDurationListViewModel _durationInfoViewModel;

        public DurationInfoDataHandler(IDurationInfoData durationInfoData, IDurationListViewModel durationListViewModel)
        {
            _durationInfoData = durationInfoData;
            _durationInfoViewModel = durationListViewModel;
        }

        public IDurationListViewModel GetDurationDataForUi()
        {
            _durationInfoViewModel.DurationDataForUi = _durationInfoData.GetDurationDataForGrid();

            return _durationInfoViewModel;
        }

        public DataTable ConvertToDataTable(List<Durationinfo> DurationInfoForExcel)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Durationinfo));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (Durationinfo item in DurationInfoForExcel)
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
