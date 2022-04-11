using ReportCoreV2.ExternalEfModel;
using ReportCoreV2.Models;
using ReportCoreV2.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.DataRepository
{
    public class ExternalApprovedScenarioData : IExternalApprovedScenarioData
    {
        private readonly ExternalReportsContext _context;
        private IExternalApprovedScenarioModel _externalApprovedScenarioModel;

        public ExternalApprovedScenarioData(ExternalReportsContext context, IExternalApprovedScenarioModel externalApprovedScenarioModel)
        {
            _context = context;
            _externalApprovedScenarioModel = externalApprovedScenarioModel;

        }

        public List<ExternalApprovedDataFields> GetExternalApprovedScenarioData(string YearFilter, string ProjectFilter)
        {
            var scenarios = (from s in _context.ManualProjectsCreatedScenarios

                             where s.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""
                             where s.Project.ToString() == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                             group s by new
                             {
                                 s.Project,
                                 s.Year,

                             } into scenarioByProject
                             select new
                             {
                                 ProjectName = scenarioByProject.Key.Project,
                                 year = scenarioByProject.Key.Year,
                                 jan = scenarioByProject.Sum(c => c.January),
                                 feb = scenarioByProject.Sum(c => c.February),
                                 mar = scenarioByProject.Sum(c => c.March),
                                 apr = scenarioByProject.Sum(c => c.April),
                                 may = scenarioByProject.Sum(c => c.May),
                                 jun = scenarioByProject.Sum(c => c.June),
                                 jul = scenarioByProject.Sum(c => c.July),
                                 aug = scenarioByProject.Sum(c => c.August),
                                 spt = scenarioByProject.Sum(c => c.September),
                                 oct = scenarioByProject.Sum(c => c.October),
                                 nov = scenarioByProject.Sum(c => c.November),
                                 dec = scenarioByProject.Sum(c => c.December),


                             }


                             into groupedprj
                             select groupedprj

                                    ).ToList();

            var orderedList = scenarios.OrderBy(i => i.ProjectName).ThenBy(i => i.year).ToList();
            foreach (var project in orderedList)
            {
                _externalApprovedScenarioModel.ExternalApprovedScenarioData.Add(new ExternalApprovedDataFields
                {

                    Project = project.ProjectName,
                    Year = project.year.ToString(),
                    January = project.jan.Value,
                    February = project.feb.Value,
                    March = project.mar.Value,
                    April = project.apr.Value,
                    May = project.may.Value,
                    June = project.jun.Value,
                    July = project.jul.Value,
                    August = project.aug.Value,
                    September = project.spt.Value,
                    October = project.oct.Value,
                    November = project.nov.Value,
                    December = project.dec.Value,


                });

            }
            return _externalApprovedScenarioModel.ExternalApprovedScenarioData;


        }

        public List<ExternalCreatedScenarioForDashboard> GetExternalApprovedScenarioForDashboard(string YearFilter)
        {
            var createdScenarios = (from s in _context.ManualProjectsCreatedScenarios

                                where s.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""
                                //where s.Project.ToString() == ProjectFilter || ProjectFilter == null || ProjectFilter == ""

                                group s by new
                                {
                                    s.Project,
                                    s.Year,



                                } into executionByProject
                                select new
                                {
                                    ProjectName = executionByProject.Key.Project,
                                    year = executionByProject.Key.Year,
                                    jan = executionByProject.Sum(c => c.January),
                                    feb = executionByProject.Sum(c => c.February),
                                    mar = executionByProject.Sum(c => c.March),
                                    apr = executionByProject.Sum(c => c.April),
                                    may = executionByProject.Sum(c => c.May),
                                    jun = executionByProject.Sum(c => c.June),
                                    jul = executionByProject.Sum(c => c.July),
                                    aug = executionByProject.Sum(c => c.August),
                                    spt = executionByProject.Sum(c => c.September),
                                    oct = executionByProject.Sum(c => c.October),
                                    nov = executionByProject.Sum(c => c.November),
                                    dec = executionByProject.Sum(c => c.December),



                                } into prj

                                select prj




                                ).ToList();

            var orderedList = createdScenarios.OrderBy(i => i.ProjectName).ThenBy(i => i.year).ToList();


            foreach (var project in orderedList)
            {
                _externalApprovedScenarioModel.ExternalApprovedScenarioForDashboard.Add(new ExternalCreatedScenarioForDashboard
                {
                    Project = project.ProjectName,
                    Year = project.year.ToString(),
                    ProjectTotal = Convert.ToInt32(project.jan) + Convert.ToInt32(project.feb) + Convert.ToInt32(project.mar) + Convert.ToInt32(project.apr) + Convert.ToInt32(project.may) + Convert.ToInt32(project.jun) + Convert.ToInt32(project.jul) + Convert.ToInt32(project.aug) + Convert.ToInt32(project.spt) + Convert.ToInt32(project.oct) + Convert.ToInt32(project.nov) + Convert.ToInt32(project.dec),

                });
            }

            return _externalApprovedScenarioModel.ExternalApprovedScenarioForDashboard;

        }
        public List<ExternalCreatedScenarioForDashboard> GetExternalLastYearApprovedScenarioForDashboard(string YearFilter)
        {
            var createdScenarios = (from s in _context.ManualProjectsCreatedScenarios

                                    where s.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""
                                    //where s.Project.ToString() == ProjectFilter || ProjectFilter == null || ProjectFilter == ""

                                    group s by new
                                    {
                                        s.Project,
                                        s.Year,



                                    } into executionByProject
                                    select new
                                    {
                                        ProjectName = executionByProject.Key.Project,
                                        year = executionByProject.Key.Year,
                                        jan = executionByProject.Sum(c => c.January),
                                        feb = executionByProject.Sum(c => c.February),
                                        mar = executionByProject.Sum(c => c.March),
                                        apr = executionByProject.Sum(c => c.April),
                                        may = executionByProject.Sum(c => c.May),
                                        jun = executionByProject.Sum(c => c.June),
                                        jul = executionByProject.Sum(c => c.July),
                                        aug = executionByProject.Sum(c => c.August),
                                        spt = executionByProject.Sum(c => c.September),
                                        oct = executionByProject.Sum(c => c.October),
                                        nov = executionByProject.Sum(c => c.November),
                                        dec = executionByProject.Sum(c => c.December),



                                    } into prj

                                    select prj




                                ).ToList();

            var orderedList = createdScenarios.OrderBy(i => i.ProjectName).ThenBy(i => i.year).ToList();


            foreach (var project in orderedList)
            {
                _externalApprovedScenarioModel.ExternalLastYearApprovedScenarioForDashboard.Add(new ExternalCreatedScenarioForDashboard
                {
                    Project = project.ProjectName,
                    Year = project.year.ToString(),
                    ProjectTotal = Convert.ToInt32(project.jan) + Convert.ToInt32(project.feb) + Convert.ToInt32(project.mar) + Convert.ToInt32(project.apr) + Convert.ToInt32(project.may) + Convert.ToInt32(project.jun) + Convert.ToInt32(project.jul) + Convert.ToInt32(project.aug) + Convert.ToInt32(project.spt) + Convert.ToInt32(project.oct) + Convert.ToInt32(project.nov) + Convert.ToInt32(project.dec),

                });
            }

            return _externalApprovedScenarioModel.ExternalLastYearApprovedScenarioForDashboard;

        }

        public List<ExternalCreatedScenarioForDashboard> GetExternalApprovedScenarioByWeekForDashboards(string YearFilter)
        {
            var approvedScenario = (from s in _context.ManualScenarioCreations
                                   
                                    where s.EntryDate.Year.ToString() == DateTime.Today.Year.ToString()
                                    group s by new
                                    {
                                        s.EntryDate.Date,
                                        s.ScenarioCreatedAmount,

                                    } into scenarioByProject
                                    //group s by DateTimeFrom.Date(s.ScenarioCreationDate) into scenarioByProject




                                    select new
                                    {

                                        date = scenarioByProject.Key.Date,
                                        total = scenarioByProject.Key.ScenarioCreatedAmount,

                                    } into prj
                                    select prj).ToList();

            foreach (var project in approvedScenario.ToList())

            {
                int WeekNum = GetWeekNumber(project.date, CultureInfo.CurrentCulture);

                _externalApprovedScenarioModel.ExternalApprovedScenariosByWeekDataForDashboard.Add(new ExternalCreatedScenarioForDashboard
                {
                    NumberOfWeek = WeekNum.ToString(),
                    DateOfTotal = project.date,
                    Year = YearFilter,
                    ProjectTotal = Convert.ToInt32(project.total),
                });

            }

            return _externalApprovedScenarioModel.ExternalApprovedScenariosByWeekDataForDashboard;

        }

        private static int GetWeekNumber(DateTime date, CultureInfo culture)
        {
            return culture.Calendar.GetWeekOfYear(date,
                culture.DateTimeFormat.CalendarWeekRule,
                culture.DateTimeFormat.FirstDayOfWeek);
        }
    }
}
