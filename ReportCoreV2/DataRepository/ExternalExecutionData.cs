
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
    public class ExternalExecutionData : IExternalExecutionData
    {
        private readonly ExternalReportsContext _context;
        private IExternalExecutionDataModel _externalExecutionDataModel;
        
        public ExternalExecutionData(ExternalReportsContext context, IExternalExecutionDataModel externalExecutionDataModel)
        {
            _context = context;
            _externalExecutionDataModel = externalExecutionDataModel;
            
        }

        public List<ExternalExecutionDataFields> GetExternalExecutionData(string YearFilter, string ProjectFilter)
        {
            var executionLog = (from s in _context.ManualProjectsExecutions

                                where s.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""
                                where s.Project.ToString() == ProjectFilter || ProjectFilter == null || ProjectFilter == ""

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

            var orderedList = executionLog.OrderBy(i => i.ProjectName).ThenBy(i => i.year).ToList();
            foreach (var project in orderedList)
            {
                _externalExecutionDataModel.ExternalExecutionData.Add(new ExternalExecutionDataFields
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
            return _externalExecutionDataModel.ExternalExecutionData;

        }

        public List<ExternalExecutionForDashboard> GetExternalExecutionDataForDashboard(string YearFilter)
        {
            var executionLog = (from s in _context.ManualProjectsExecutions

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

            var orderedList = executionLog.OrderBy(i => i.ProjectName).ThenBy(i => i.year).ToList();


            foreach (var project in orderedList)
            {
                _externalExecutionDataModel.ExternalExecutionDataForDashboard.Add(new ExternalExecutionForDashboard
                {
                    Project = project.ProjectName,
                    Year = project.year.ToString(),
                    ProjectTotal = Convert.ToInt32(project.jan) + Convert.ToInt32(project.feb) + Convert.ToInt32(project.mar) + Convert.ToInt32(project.apr) + Convert.ToInt32(project.may) + Convert.ToInt32(project.jun) + Convert.ToInt32(project.jul) + Convert.ToInt32(project.aug) + Convert.ToInt32(project.spt) + Convert.ToInt32(project.oct) + Convert.ToInt32(project.nov) + Convert.ToInt32(project.dec),

                });
            }
            
            return _externalExecutionDataModel.ExternalExecutionDataForDashboard;

        }
        public List<ExternalExecutionForDashboard> GetLastYearExternalExecutionDataForDashboard(string YearFilter)
        {
            var executionLog = (from s in _context.ManualProjectsExecutions

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

            var orderedList = executionLog.OrderBy(i => i.ProjectName).ThenBy(i => i.year).ToList();


            foreach (var project in orderedList)
            {
                _externalExecutionDataModel.ExternalLastYearExecutionDataForDashboard.Add(new ExternalExecutionForDashboard
                {
                    Project = project.ProjectName,
                    Year = project.year.ToString(),
                    ProjectTotal = Convert.ToInt32(project.jan) + Convert.ToInt32(project.feb) + Convert.ToInt32(project.mar) + Convert.ToInt32(project.apr) + Convert.ToInt32(project.may) + Convert.ToInt32(project.jun) + Convert.ToInt32(project.jul) + Convert.ToInt32(project.aug) + Convert.ToInt32(project.spt) + Convert.ToInt32(project.oct) + Convert.ToInt32(project.nov) + Convert.ToInt32(project.dec),

                });
            }
            
            return _externalExecutionDataModel.ExternalLastYearExecutionDataForDashboard;

        }
        public List<ExternalExecutionForDashboard> GetCurrentMonthExternalExecutionDataForDashboard()
        {

            var executionLog = (from s in _context.ManualExecutions

                                where s.EntryDate.Year == DateTime.Today.Year
                                where s.EntryDate.Month == DateTime.Today.Month

                                group s by new
                                {
                                    s.ProjectName,


                                } into executionByProject
                                select new
                                {
                                    ProjectName = executionByProject.Key.ProjectName,
                                    total = executionByProject.Where(c => c.EntryDate.Month == DateTime.Today.Month).Sum(i => i.ScenarioExecutionAmount),

                                } into prj

                                select prj).ToList();

       
            var orderedList = executionLog.OrderBy(i => i.ProjectName).ToList();


            foreach (var project in orderedList)
            {
                _externalExecutionDataModel.ExternalCurrentMonthExecutionDataForDashboard.Add(new ExternalExecutionForDashboard
                {
                    Project = project.ProjectName,
                    ProjectTotal = Convert.ToInt32(project.total),

                });
            }

            return _externalExecutionDataModel.ExternalCurrentMonthExecutionDataForDashboard;

        }
        public List<ExternalExecutionForDashboard> GetExternalExecutionByWeekForDashboards(string YearFilter)
        {
            var executedScenario = (from s in _context.ManualExecutions

                                    where s.EntryDate.Year.ToString() == DateTime.Today.Year.ToString()
                                    group s by new
                                    {
                                        s.EntryDate.Date,
                                        s.ScenarioExecutionAmount,

                                    } into scenarioByProject
                                    //group s by DateTimeFrom.Date(s.ScenarioCreationDate) into scenarioByProject




                                    select new
                                    {

                                        date = scenarioByProject.Key.Date,
                                        total = scenarioByProject.Key.ScenarioExecutionAmount,

                                    } into prj
                                    select prj).ToList();

            foreach (var project in executedScenario.ToList())

            {
                int WeekNum = GetWeekNumber(project.date, CultureInfo.CurrentCulture);

                _externalExecutionDataModel.ExternalExecutionByWeekDataForDashboard.Add(new ExternalExecutionForDashboard
                {
                    NumberOfWeek = WeekNum.ToString(),
                    DateOfTotal = project.date,
                    Year = YearFilter,
                    ProjectTotal = Convert.ToInt32(project.total),
                });

            }

            return _externalExecutionDataModel.ExternalExecutionByWeekDataForDashboard;

        }


        public List<ExternalExecutionForDashboard> GutLastYearSumPerProject()
        {
            
                
                foreach (var project in _context.ManualProjectsExecutions)
                {
                _externalExecutionDataModel.ExternalExecutionDataForDashboard.Add(new ExternalExecutionForDashboard
                    {
                        Project = project.Project,
                        Year = project.Year.ToString(),
                        ProjectTotal = Convert.ToInt32(project.January) + Convert.ToInt32(project.February) + Convert.ToInt32(project.March) + Convert.ToInt32(project.April) + Convert.ToInt32(project.May) + Convert.ToInt32(project.June) + Convert.ToInt32(project.July) + Convert.ToInt32(project.August) + Convert.ToInt32(project.September) + Convert.ToInt32(project.October) + Convert.ToInt32(project.November) + Convert.ToInt32(project.December),

                    });

                }

                return _externalExecutionDataModel.ExternalExecutionDataForDashboard;
            
        }

        public List<ExternalExecutionForDashboard> GutCurrentYearSumPerProject(string YearFilter)
        {


            foreach (var project in _context.ManualProjectsExecutions)
            {
                _externalExecutionDataModel.ExternalExecutionDataForDashboard.Add(new ExternalExecutionForDashboard
                {
                    Project = project.Project,
                    Year = project.Year.ToString(),
                    ProjectTotal = Convert.ToInt32(project.January) + Convert.ToInt32(project.February) + Convert.ToInt32(project.March) + Convert.ToInt32(project.April) + Convert.ToInt32(project.May) + Convert.ToInt32(project.June) + Convert.ToInt32(project.July) + Convert.ToInt32(project.August) + Convert.ToInt32(project.September) + Convert.ToInt32(project.October) + Convert.ToInt32(project.November) + Convert.ToInt32(project.December),

                });

            }

            return _externalExecutionDataModel.ExternalExecutionDataForDashboard;

        }

        private static int GetWeekNumber(DateTime date, CultureInfo culture)
        {
            return culture.Calendar.GetWeekOfYear(date,
                culture.DateTimeFormat.CalendarWeekRule,
                culture.DateTimeFormat.FirstDayOfWeek);
        }
    }
}
