using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models.ViewModel
{
    public class DataPointsForGraphsViewModel
    {
        private string _dataPointsAsStringForUI;
        private string _dataLabelsAsStringForUI;

        public DataPointsForGraphsViewModel()
        {
            DataPointsList = new List<DataPoints>();
        }
        public string ProjectTotal { get; set; }
        public string TotalScenariosinCycle { get; set; }
        public string PassScenariosinCycle { get; set; }
        public string TotalExecuteinCycle { get; set; }
        public string Passrate { get; set; }
        public string CycleNameForGraph { get; set; }
        public string ProjectName { get; set; }
        public string YearOfData { get; set; }
        public Guid GuidID { get; set; }
        public DateTime DateOfData { get; set; }
        public List<DataPoints> DataPointsList { get; set; }

        public string DataPointsAsStringForUI
        {
            get
            {
                _dataPointsAsStringForUI = string.Empty;
                _dataPointsAsStringForUI += "[";

                for (int i = 0; i < DataPointsList.Count; i++)
                {

                    _dataPointsAsStringForUI += "'";
                    _dataPointsAsStringForUI += DataPointsList[i].ColumnValue;
                    _dataPointsAsStringForUI += "'";

                    if (i != DataPointsList.Count - 1)
                    {
                        _dataPointsAsStringForUI += ",";
                    }

                }
                _dataPointsAsStringForUI += "]";

                return _dataPointsAsStringForUI;

            }
        }

        public string DataLabelsAsStringForUI
        {
            get
            {
                _dataLabelsAsStringForUI = string.Empty;
                _dataLabelsAsStringForUI += "[";

                for (int i = 0; i < DataPointsList.Count; i++)
                {

                    _dataLabelsAsStringForUI += "'";
                    _dataLabelsAsStringForUI += DataPointsList[i].ColumnLabel;
                    _dataLabelsAsStringForUI += "'";

                    if (i != DataPointsList.Count - 1)
                    {
                        _dataLabelsAsStringForUI += ",";
                    }

                }
                _dataLabelsAsStringForUI += "]";

                return _dataLabelsAsStringForUI;

            }
        }


    }
}
