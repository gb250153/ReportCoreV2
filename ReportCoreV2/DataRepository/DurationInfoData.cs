using ReportCoreV2.ATCEfModel;
using ReportCoreV2.Models;
using ReportCoreV2.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.DataRepository
{
    public class DurationInfoData : IDurationInfoData
    {
        private readonly ATCContext _context;
        private IDurationInfoModel _durationInfoModel;


        public DurationInfoData(ATCContext context, IDurationInfoModel durationInfoModel)
        {
            _context = context;
            _durationInfoModel = durationInfoModel;
        }
        public List<Durationinfo> GetDurationDataForGrid()
        {
            var durationtable = (from s in _context.DurationResults
                                 
                                 select s).ToList();


            foreach (var item in durationtable)
            {
                _durationInfoModel.Duration.Add(new Durationinfo
                {
                    Host = item.Host,
                    SetName = item.Setname,
                    SetContent = item.SetContent,
                    SetDuration = item.SetDuration,
                    SetTestRun = item.SetTestRun,
                    SetTimeDate = item.SetTimeDate,
                    ScenarioDuration = item.ScenarioDuration,
                    ScenarioName = item.ScenarioName,
                    ScenarioNumber = item.ScenarioNumber,
                    ScenarioTimeDate = item.ScenarioTimeDate,
                    ActionTimeDate = item.ActionTimeDate,
                    ActionActual = item.ActionActual,
                    ActionDuration = item.ActionDuration,
                    ActionName = item.ActionName,
                    ActionNumber = item.ActionNumber,

                });

            }

            return _durationInfoModel.Duration;
        }

    }

}
