using ReportCoreV2.ATCEfModel;
using ReportCoreV2.Models;
using ReportCoreV2.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.DataRepository
{
    public class ApprovedScenarioData : IApprovedScenarioData
    {
        private readonly ATCContext _context;
        private IApprovedScenarioModel _approvedScenarioModel;
        private IExternalApprovedScenarioData _externalApprovedScenarioData;
        public ApprovedScenarioData(ATCContext context, IApprovedScenarioModel approvedScenarioModel, IExternalApprovedScenarioData externalApprovedScenarioData)
        {
            _context = context;
            _approvedScenarioModel = approvedScenarioModel;
            _externalApprovedScenarioData = externalApprovedScenarioData;
        }
        public List<ApprovedScenariosFields> GetApprovedScenariosDataForGrid(string StateFilter, string YearFilter, string ProjectFilter)
        {
            MonthlyDataAggregate Sumall = new MonthlyDataAggregate();

            var scenarios = (from s in _context.TblSysScenarios
                             where s.ScenarioStatus == StateFilter
                             where s.ScenarioCreationDate.Value.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""
                             where s.ProjectOwner.ToString() == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                             group s by new
                             {
                                 s.ProjectOwner,
                                 s.ScenarioCreationDate.Value.Year,
                                 s.ScenarioCreationDate.Value.Month,

                             } into scenarioByProject
                             select new
                             {
                                 ProjectName = scenarioByProject.Key.ProjectOwner,
                                 year = scenarioByProject.Key.Year,
                                 jan = scenarioByProject.Where(c => c.ScenarioCreationDate.Value.Month == 1).Count(),
                                 feb = scenarioByProject.Where(c => c.ScenarioCreationDate.Value.Month == 2).Count(),
                                 mar = scenarioByProject.Where(c => c.ScenarioCreationDate.Value.Month == 3).Count(),
                                 apr = scenarioByProject.Where(c => c.ScenarioCreationDate.Value.Month == 4).Count(),
                                 may = scenarioByProject.Where(c => c.ScenarioCreationDate.Value.Month == 5).Count(),
                                 jun = scenarioByProject.Where(c => c.ScenarioCreationDate.Value.Month == 6).Count(),
                                 jul = scenarioByProject.Where(c => c.ScenarioCreationDate.Value.Month == 7).Count(),
                                 aug = scenarioByProject.Where(c => c.ScenarioCreationDate.Value.Month == 8).Count(),
                                 spt = scenarioByProject.Where(c => c.ScenarioCreationDate.Value.Month == 9).Count(),
                                 oct = scenarioByProject.Where(c => c.ScenarioCreationDate.Value.Month == 10).Count(),
                                 nov = scenarioByProject.Where(c => c.ScenarioCreationDate.Value.Month == 11).Count(),
                                 dec = scenarioByProject.Where(c => c.ScenarioCreationDate.Value.Month == 12).Count(),


                             }


                             into groupedprj
                             select groupedprj

                                 ).ToList();

            var externalProjectData = _externalApprovedScenarioData.GetExternalApprovedScenarioData(YearFilter, ProjectFilter);
            var groupedList = scenarios.GroupBy(x => new { x.ProjectName, x.year });

            foreach (var project in groupedList)

            {
                foreach (var item in project)
                {

                    Sumall.Sum_January += Convert.ToInt32(item.jan);
                    Sumall.Sum_February += Convert.ToInt32(item.feb);
                    Sumall.Sum_March += Convert.ToInt32(item.mar);
                    Sumall.Sum_April += Convert.ToInt32(item.apr);
                    Sumall.Sum_May += Convert.ToInt32(item.may);
                    Sumall.Sum_June += Convert.ToInt32(item.jun);
                    Sumall.Sum_July += Convert.ToInt32(item.jul);
                    Sumall.Sum_August += Convert.ToInt32(item.aug);
                    Sumall.Sum_September += Convert.ToInt32(item.spt);
                    Sumall.Sum_October += Convert.ToInt32(item.oct);
                    Sumall.Sum_November += Convert.ToInt32(item.nov);
                    Sumall.Sum_December += Convert.ToInt32(item.dec);

                }



                _approvedScenarioModel.ApprovedScenariosData.Add(new ApprovedScenariosFields
                {
                    ProjectOwner = project.Key.ProjectName,
                    Year = project.Key.year.ToString(),
                    January = Convert.ToInt32(Sumall.Sum_January),
                    February = Convert.ToInt32(Sumall.Sum_February),
                    March = Convert.ToInt32(Sumall.Sum_March),
                    April = Convert.ToInt32(Sumall.Sum_April),
                    May = Convert.ToInt32(Sumall.Sum_May),
                    June = Convert.ToInt32(Sumall.Sum_June),
                    July = Convert.ToInt32(Sumall.Sum_July),
                    August = Convert.ToInt32(Sumall.Sum_August),
                    September = Convert.ToInt32(Sumall.Sum_September),
                    October = Convert.ToInt32(Sumall.Sum_October),
                    November = Convert.ToInt32(Sumall.Sum_November),
                    December = Convert.ToInt32(Sumall.Sum_December),
                    ProjectTotal = Convert.ToInt32(Sumall.Sum_December + Sumall.Sum_November + Sumall.Sum_October + Sumall.Sum_September + Sumall.Sum_August + Sumall.Sum_July + Sumall.Sum_June + Sumall.Sum_May + Sumall.Sum_April + Sumall.Sum_March + Sumall.Sum_February + Sumall.Sum_January)


                });


                Sumall.Sum_January = 0;
                Sumall.Sum_February = 0;
                Sumall.Sum_March = 0;
                Sumall.Sum_April = 0;
                Sumall.Sum_May = 0;
                Sumall.Sum_June = 0;
                Sumall.Sum_July = 0;
                Sumall.Sum_August = 0;
                Sumall.Sum_September = 0;
                Sumall.Sum_October = 0;
                Sumall.Sum_November = 0;
                Sumall.Sum_December = 0;


            }

            if (externalProjectData != null)
            {
                foreach (var manualProject in externalProjectData)
                {
                    _approvedScenarioModel.ApprovedScenariosData.Add(new ApprovedScenariosFields
                    {

                        ProjectOwner = manualProject.Project,
                        Year = manualProject.Year.ToString(),
                        January = manualProject.January,
                        February = manualProject.February,
                        March = manualProject.March,
                        April = manualProject.April,
                        May = manualProject.May,
                        June = manualProject.June,
                        July = manualProject.July,
                        August = manualProject.August,
                        September = manualProject.September,
                        October = manualProject.October,
                        November = manualProject.November,
                        December = manualProject.December,

                    });
                }
            }
            return _approvedScenarioModel.ApprovedScenariosData;
        }

    }
}
