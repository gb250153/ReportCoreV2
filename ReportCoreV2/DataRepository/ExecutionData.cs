using ReportCoreV2.ATCEfModel;
using ReportCoreV2.Models;
using ReportCoreV2.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.DataRepository
{
    public class ExecutionData : IExecutionData
    {
        private readonly ATCContext _context;
        private IExecutionLogModel _executionLogModel;
        private IExternalExecutionData _externalExecutionData;

        public ExecutionData(ATCContext context, IExecutionLogModel executionLogModel, IExternalExecutionData externalExecutionData)
        {
            _context = context;
            _executionLogModel = executionLogModel;
            _externalExecutionData = externalExecutionData;
        }

        public List<ExecutionLogFields> GetExecutionDataForGrid(string ProcessFilter, string YearFilter, string ProjectFilter)
        {


            MonthlyDataAggregate Sumall = new MonthlyDataAggregate();


            var executionLog = (from s in _context.TblSysExecutionLogs
                                where s.Process == ProcessFilter
                                where s.DateReq.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""
                                where s.Project.ToString() == ProjectFilter || ProjectFilter == null || ProjectFilter == ""

                                group s by new
                                {
                                    s.Project,
                                    s.DateReq.Year,
                                    s.DateReq.Month,


                                } into executionByProject
                                select new
                                {
                                    ProjectName = executionByProject.Key.Project,
                                    year = executionByProject.Key.Year,
                                    jan = executionByProject.Where(c => c.DateReq.Month == 1).Sum(i => i.ScenarioCounterExecuted),
                                    feb = executionByProject.Where(c => c.DateReq.Month == 2).Sum(i => i.ScenarioCounterExecuted),
                                    mar = executionByProject.Where(c => c.DateReq.Month == 3).Sum(i => i.ScenarioCounterExecuted),
                                    apr = executionByProject.Where(c => c.DateReq.Month == 4).Sum(i => i.ScenarioCounterExecuted),
                                    may = executionByProject.Where(c => c.DateReq.Month == 5).Sum(i => i.ScenarioCounterExecuted),
                                    jun = executionByProject.Where(c => c.DateReq.Month == 6).Sum(i => i.ScenarioCounterExecuted),
                                    jul = executionByProject.Where(c => c.DateReq.Month == 7).Sum(i => i.ScenarioCounterExecuted),
                                    aug = executionByProject.Where(c => c.DateReq.Month == 8).Sum(i => i.ScenarioCounterExecuted),
                                    spt = executionByProject.Where(c => c.DateReq.Month == 9).Sum(i => i.ScenarioCounterExecuted),
                                    oct = executionByProject.Where(c => c.DateReq.Month == 10).Sum(i => i.ScenarioCounterExecuted),
                                    nov = executionByProject.Where(c => c.DateReq.Month == 11).Sum(i => i.ScenarioCounterExecuted),
                                    dec = executionByProject.Where(c => c.DateReq.Month == 12).Sum(i => i.ScenarioCounterExecuted),


                                } into prj

                                select prj




                             ).ToList();

            var executionLogArchive = (from s in _context.TblSysExecutionLogArchives
                                       where s.Process == ProcessFilter
                                       where s.DateReq.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""
                                       where s.Project.ToString() == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                       group s by new
                                       {
                                           s.Project,
                                           s.DateReq.Year,
                                           s.DateReq.Month,

                                       } into executionByProject
                                       select new
                                       {
                                           ProjectName = executionByProject.Key.Project,
                                           year = executionByProject.Key.Year,
                                           jan = executionByProject.Where(c => c.DateReq.Month == 1).Sum(i => i.ScenarioCounterExecuted),
                                           feb = executionByProject.Where(c => c.DateReq.Month == 2).Sum(i => i.ScenarioCounterExecuted),
                                           mar = executionByProject.Where(c => c.DateReq.Month == 3).Sum(i => i.ScenarioCounterExecuted),
                                           apr = executionByProject.Where(c => c.DateReq.Month == 4).Sum(i => i.ScenarioCounterExecuted),
                                           may = executionByProject.Where(c => c.DateReq.Month == 5).Sum(i => i.ScenarioCounterExecuted),
                                           jun = executionByProject.Where(c => c.DateReq.Month == 6).Sum(i => i.ScenarioCounterExecuted),
                                           jul = executionByProject.Where(c => c.DateReq.Month == 7).Sum(i => i.ScenarioCounterExecuted),
                                           aug = executionByProject.Where(c => c.DateReq.Month == 8).Sum(i => i.ScenarioCounterExecuted),
                                           spt = executionByProject.Where(c => c.DateReq.Month == 9).Sum(i => i.ScenarioCounterExecuted),
                                           oct = executionByProject.Where(c => c.DateReq.Month == 10).Sum(i => i.ScenarioCounterExecuted),
                                           nov = executionByProject.Where(c => c.DateReq.Month == 11).Sum(i => i.ScenarioCounterExecuted),
                                           dec = executionByProject.Where(c => c.DateReq.Month == 12).Sum(i => i.ScenarioCounterExecuted),


                                       } into prj

                                       select prj




                             ).ToList();

            var externalProjectData = _externalExecutionData.GetExternalExecutionData(YearFilter, ProjectFilter);
            var joinedlist = executionLog.Union(executionLogArchive).Distinct();
            var groupedList = from p in joinedlist
                              group p by new
                              {
                                  p.ProjectName,
                                  p.year,
                              } into newGroup

                              select newGroup;
            var orderedList = groupedList.OrderBy(i => i.Key.ProjectName).ThenBy(i => i.Key.year).ToList();

            foreach (var project in orderedList.ToList())

            {
                foreach (var item in project.ToList())
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


                _executionLogModel.ExecutionLogData.Add(new ExecutionLogFields
                {
                    Projectid = Guid.NewGuid(),
                    Project = project.Key.ProjectName,
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
                    _executionLogModel.ExecutionLogData.Add(new ExecutionLogFields
                    {
                        Projectid = Guid.NewGuid(),
                        Project = manualProject.Project,
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
            return _executionLogModel.ExecutionLogData;



        }

        public List<ExecutionLogFieldsForExcel> GetExecutionDataForExport(string ProcessFilter, string YearFilter, string ProjectFilter)
        {

            MonthlyDataAggregate Sumall = new MonthlyDataAggregate();

            var executionLog = (from s in _context.TblSysExecutionLogs
                                where s.Process == ProcessFilter
                                where s.DateReq.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""
                                where s.Project.ToString() == ProjectFilter || ProjectFilter == null || ProjectFilter == ""

                                group s by new
                                {
                                    s.Project,
                                    s.DateReq.Year,
                                    s.DateReq.Month,


                                } into executionByProject
                                select new
                                {
                                    ProjectName = executionByProject.Key.Project,
                                    year = executionByProject.Key.Year,
                                    jan = executionByProject.Where(c => c.DateReq.Month == 1).Sum(i => i.ScenarioCounterExecuted),
                                    feb = executionByProject.Where(c => c.DateReq.Month == 2).Sum(i => i.ScenarioCounterExecuted),
                                    mar = executionByProject.Where(c => c.DateReq.Month == 3).Sum(i => i.ScenarioCounterExecuted),
                                    apr = executionByProject.Where(c => c.DateReq.Month == 4).Sum(i => i.ScenarioCounterExecuted),
                                    may = executionByProject.Where(c => c.DateReq.Month == 5).Sum(i => i.ScenarioCounterExecuted),
                                    jun = executionByProject.Where(c => c.DateReq.Month == 6).Sum(i => i.ScenarioCounterExecuted),
                                    jul = executionByProject.Where(c => c.DateReq.Month == 7).Sum(i => i.ScenarioCounterExecuted),
                                    aug = executionByProject.Where(c => c.DateReq.Month == 8).Sum(i => i.ScenarioCounterExecuted),
                                    spt = executionByProject.Where(c => c.DateReq.Month == 9).Sum(i => i.ScenarioCounterExecuted),
                                    oct = executionByProject.Where(c => c.DateReq.Month == 10).Sum(i => i.ScenarioCounterExecuted),
                                    nov = executionByProject.Where(c => c.DateReq.Month == 11).Sum(i => i.ScenarioCounterExecuted),
                                    dec = executionByProject.Where(c => c.DateReq.Month == 12).Sum(i => i.ScenarioCounterExecuted),


                                } into prj

                                select prj




                             ).ToList();

            var executionLogArchive = (from s in _context.TblSysExecutionLogArchives
                                       where s.Process == ProcessFilter
                                       where s.DateReq.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""
                                       where s.Project.ToString() == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                       group s by new
                                       {
                                           s.Project,
                                           s.DateReq.Year,
                                           s.DateReq.Month,

                                       } into executionByProject
                                       select new
                                       {
                                           ProjectName = executionByProject.Key.Project,
                                           year = executionByProject.Key.Year,
                                           jan = executionByProject.Where(c => c.DateReq.Month == 1).Sum(i => i.ScenarioCounterExecuted),
                                           feb = executionByProject.Where(c => c.DateReq.Month == 2).Sum(i => i.ScenarioCounterExecuted),
                                           mar = executionByProject.Where(c => c.DateReq.Month == 3).Sum(i => i.ScenarioCounterExecuted),
                                           apr = executionByProject.Where(c => c.DateReq.Month == 4).Sum(i => i.ScenarioCounterExecuted),
                                           may = executionByProject.Where(c => c.DateReq.Month == 5).Sum(i => i.ScenarioCounterExecuted),
                                           jun = executionByProject.Where(c => c.DateReq.Month == 6).Sum(i => i.ScenarioCounterExecuted),
                                           jul = executionByProject.Where(c => c.DateReq.Month == 7).Sum(i => i.ScenarioCounterExecuted),
                                           aug = executionByProject.Where(c => c.DateReq.Month == 8).Sum(i => i.ScenarioCounterExecuted),
                                           spt = executionByProject.Where(c => c.DateReq.Month == 9).Sum(i => i.ScenarioCounterExecuted),
                                           oct = executionByProject.Where(c => c.DateReq.Month == 10).Sum(i => i.ScenarioCounterExecuted),
                                           nov = executionByProject.Where(c => c.DateReq.Month == 11).Sum(i => i.ScenarioCounterExecuted),
                                           dec = executionByProject.Where(c => c.DateReq.Month == 12).Sum(i => i.ScenarioCounterExecuted),


                                       } into prj

                                       select prj




                             ).ToList();

            var externalProjectData = _externalExecutionData.GetExternalExecutionData(YearFilter, ProjectFilter);
            var joinedlist = executionLog.Union(executionLogArchive).Distinct();
            var groupedList = from p in joinedlist
                              group p by new
                              {
                                  p.ProjectName,
                                  p.year,
                              } into newGroup

                              select newGroup;
            var orderedList = groupedList.OrderBy(i => i.Key.ProjectName).ThenBy(i => i.Key.year).ToList();

            foreach (var project in orderedList.ToList())

            {
                foreach (var item in project.ToList())
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


                _executionLogModel.ExecutionLogDataForExport.Add(new ExecutionLogFieldsForExcel
                {

                    Project = project.Key.ProjectName,
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
                    _executionLogModel.ExecutionLogData.Add(new ExecutionLogFields
                    {
                        Projectid = Guid.NewGuid(),
                        Project = manualProject.Project,
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

            return _executionLogModel.ExecutionLogDataForExport;



        }
    }
}
