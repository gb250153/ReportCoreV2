using ReportCoreV2.ATCEfModel;
using ReportCoreV2.Models;
using ReportCoreV2.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace ReportCoreV2.DataRepository
{
    public class DashboardData : IDashboardData
    {
        private readonly ATCContext _context;

        private IDashboardModel _dashboardModel;
        private IExternalExecutionData _externalExecutionData;
        private IExternalApprovedScenarioData _externalApprovedScenarioData;


        public DashboardData(ATCContext context, IDashboardModel dashboardModel, IExternalExecutionData externalExecutionData, IExternalApprovedScenarioData externalApprovedScenarioData)
        {
            _context = context;
            _dashboardModel = dashboardModel;
            _externalExecutionData = externalExecutionData;
            _externalApprovedScenarioData = externalApprovedScenarioData;
        }



        public List<ExecutionLogFieldsForDashboard> GetLastYearExecutionDataForDashboardChart(string ProcessFilter, string YearFilter)
        {

            ExecutionDatasummeryForChart _SumProject = new ExecutionDatasummeryForChart();
            //DashboardModel dashboardModel = new DashboardModel();


            int lastyear = Convert.ToInt32(YearFilter);
            var executionLog = (from s in _context.TblSysExecutionLogs
                                where s.Process == ProcessFilter
                                where s.DateReq.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""
                                //where s.DateReq.Year == DateTime.Today.Year

                                group s by new
                                {
                                    s.Project,
                                    s.DateReq.Year,

                                } into executionByProject
                                select new
                                {
                                    ProjectName = executionByProject.Key.Project,
                                    year = executionByProject.Key.Year,
                                    total = executionByProject.Where(c => c.DateReq.Year == lastyear).Sum(i => i.ScenarioCounterExecuted),

                                } into prj

                                select prj).ToList();

            var executionLogArchive = (from s in _context.TblSysExecutionLogArchives
                                       where s.Process == ProcessFilter
                                       where s.DateReq.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""
                                       //where s.DateReq.Year == DateTime.Today.Year

                                       group s by new
                                       {
                                           s.Project,
                                           s.DateReq.Year,

                                       } into executionByProject
                                       select new
                                       {
                                           ProjectName = executionByProject.Key.Project,
                                           year = executionByProject.Key.Year,
                                           total = executionByProject.Where(c => c.DateReq.Year == lastyear).Sum(i => i.ScenarioCounterExecuted),

                                       } into prj

                                       select prj).ToList();

            var externalProjectData = _externalExecutionData.GetLastYearExternalExecutionDataForDashboard(YearFilter);
            var joinedList = executionLog.Union(executionLogArchive).Distinct();
            var groupedList = from p in joinedList
                              group p by new
                              {
                                  p.ProjectName,
                                  p.year,

                              } into newGroup

                              select newGroup;

            groupedList = groupedList.OrderBy(x => x.Key.ProjectName).ToList();

            var orderedList = groupedList.OrderBy(i => i.Key.ProjectName).ThenBy(i => i.Key.year).ToList();
            // executionLog = executionLog.OrderBy(x => x.ProjectName).ToList();

            foreach (var project in orderedList.ToList())

            {
                foreach (var item in project)
                {
                    _SumProject.ProjectSum += Convert.ToInt32(item.total);
                }

                _dashboardModel.LastYearExecutionLogDataForDashboard.Add(new ExecutionLogFieldsForDashboard
                {
                    //Projectid = Guid.NewGuid(),
                    Project = project.Key.ProjectName,
                    Year = project.Key.year.ToString(),
                    ProjectTotal = Convert.ToInt32(_SumProject.ProjectSum),
                });
                _SumProject.ProjectSum = 0;

            }
            if (externalProjectData != null)
            {
                foreach (var project in externalProjectData)
                {
                    _dashboardModel.LastYearExecutionLogDataForDashboard.Add(new ExecutionLogFieldsForDashboard
                    {

                        Project = project.Project,
                        Year = project.Year,
                        ProjectTotal = project.ProjectTotal,
                    });
                }

            }

            return _dashboardModel.LastYearExecutionLogDataForDashboard;


        }

        public List<ExecutionLogFieldsForDashboard> GetExecutionDataForDashboardChart(string ProcessFilter, string YearFilter)
        {
            ExecutionDatasummeryForChart _SumProject = new ExecutionDatasummeryForChart();
           

            var executionLog = (from s in _context.TblSysExecutionLogs
                                where s.Process == ProcessFilter
                                where s.DateReq.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""
                                //where s.DateReq.Year == DateTime.Today.Year

                                group s by new
                                {
                                    s.Project,
                                    s.DateReq.Year,

                                } into executionByProject
                                select new
                                {
                                    ProjectName = executionByProject.Key.Project,
                                    year = executionByProject.Key.Year,
                                    total = executionByProject.Where(c => c.DateReq.Year == DateTime.Today.Year).Sum(i => i.ScenarioCounterExecuted),

                                } into prj

                                select prj).ToList();

            var executionLogArchive = (from s in _context.TblSysExecutionLogArchives
                                       where s.Process == ProcessFilter
                                       where s.DateReq.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""
                                       //where s.DateReq.Year == DateTime.Today.Year

                                       group s by new
                                       {
                                           s.Project,
                                           s.DateReq.Year,

                                       } into executionByProject
                                       select new
                                       {
                                           ProjectName = executionByProject.Key.Project,
                                           year = executionByProject.Key.Year,
                                           total = executionByProject.Where(c => c.DateReq.Year == DateTime.Today.Year).Sum(i => i.ScenarioCounterExecuted),

                                       } into prj

                                       select prj).ToList();

            var ExternalProjectData = _externalExecutionData.GetExternalExecutionDataForDashboard(YearFilter);

            var joinedList = executionLog.Union(executionLogArchive).Distinct();



            var groupedList = from p in joinedList
                              group p by new
                              {
                                  p.ProjectName,
                                  p.year,

                              } into newGroup

                              select newGroup;

            groupedList = groupedList.OrderBy(x => x.Key.ProjectName).ToList();


            var orderedList = groupedList.OrderBy(i => i.Key.ProjectName).ThenBy(i => i.Key.year).ToList();
            // executionLog = executionLog.OrderBy(x => x.ProjectName).ToList();

            foreach (var project in orderedList.ToList())

            {
                foreach (var item in project)
                {
                    _SumProject.ProjectSum += Convert.ToInt32(item.total);
                }

                _dashboardModel.ExecutionLogDataForDashboard.Add(new ExecutionLogFieldsForDashboard
                {
                    //Projectid = Guid.NewGuid(),
                    Project = project.Key.ProjectName,
                    Year = project.Key.year.ToString(),
                    ProjectTotal = Convert.ToInt32(_SumProject.ProjectSum),
                });
                _SumProject.ProjectSum = 0;

            }



            foreach (var project in ExternalProjectData)
            {
                _dashboardModel.ExecutionLogDataForDashboard.Add(new ExecutionLogFieldsForDashboard
                {

                    Project = project.Project,
                    Year = project.Year,
                    ProjectTotal = project.ProjectTotal,
                });
            }



            return _dashboardModel.ExecutionLogDataForDashboard;

        }

        public List<ExecutionLogFieldsForDashboard> GetcurrentMonthExecutionDataForDashboardChart(string ProcessFilter)
        {



            DashboardModel dashboardModel = new DashboardModel();


            var executionLog = (from s in _context.TblSysExecutionLogs
                                where s.Process == ProcessFilter
                                where s.DateReq.Year == DateTime.Today.Year
                                where s.DateReq.Month == DateTime.Today.Month

                                group s by new
                                {
                                    s.Project,


                                } into executionByProject
                                select new
                                {
                                    ProjectName = executionByProject.Key.Project,
                                    total = executionByProject.Where(c => c.DateReq.Month == DateTime.Today.Month).Sum(i => i.ScenarioCounterExecuted),

                                } into prj

                                select prj).ToList();
            var ExternalProjectData = _externalExecutionData.GetCurrentMonthExternalExecutionDataForDashboard();
            executionLog = executionLog.OrderBy(x => x.ProjectName).ToList();

            foreach (var project in executionLog.ToList())

            {
                _dashboardModel.CurrentMonthExecutionLogDataForDashboard.Add(new ExecutionLogFieldsForDashboard
                {
                    // Projectid = Guid.NewGuid(),
                    Project = project.ProjectName,

                    ProjectTotal = Convert.ToInt32(project.total),
                });

            }
            foreach (var project in ExternalProjectData)
            {
                _dashboardModel.CurrentMonthExecutionLogDataForDashboard.Add(new ExecutionLogFieldsForDashboard
                {

                    Project = project.Project,

                    ProjectTotal = project.ProjectTotal,
                });
            }

            return _dashboardModel.CurrentMonthExecutionLogDataForDashboard;

        }

        public List<ExecutionLogFieldsForDashboard> GetLastYearApprovedScenarioDataForDashboardChart(string ProcessFilter, string YearFilter)
        {


            int lastyear = Convert.ToInt32(YearFilter);
            var approvedScenario = (from s in _context.TblSysScenarios
                                    where s.ScenarioStatus == ProcessFilter
                                    where s.ScenarioCreationDate.Value.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""


                                    group s by new
                                    {
                                        s.ProjectOwner,


                                    } into scenarioByProject
                                    select new
                                    {
                                        ProjectName = scenarioByProject.Key.ProjectOwner,
                                        total = scenarioByProject.Where(c => c.ScenarioCreationDate.Value.Year == lastyear).Count(),

                                    } into prj

                                    select prj).ToList();
            approvedScenario = approvedScenario.OrderBy(x => x.ProjectName).ToList();

            var ExternalProjectData = _externalApprovedScenarioData.GetExternalLastYearApprovedScenarioForDashboard(YearFilter);

            foreach (var project in approvedScenario.ToList())

            {
                _dashboardModel.ApprovedLastyearScenariosDataForDashboard.Add(new ExecutionLogFieldsForDashboard
                {
                    //Projectid = Guid.NewGuid(),
                    Project = project.ProjectName,
                    Year = YearFilter,
                    ProjectTotal = Convert.ToInt32(project.total),
                });

            }

            foreach (var project in ExternalProjectData)
            {
                _dashboardModel.ApprovedLastyearScenariosDataForDashboard.Add(new ExecutionLogFieldsForDashboard
                {

                    Project = project.Project,
                    Year = project.Year,
                    ProjectTotal = project.ProjectTotal,
                });
            }


            return _dashboardModel.ApprovedLastyearScenariosDataForDashboard;

        }

        public List<ExecutionLogFieldsForDashboard> GetApprovedScenarioDataForDashboardChart(string ProcessFilter, string YearFilter)
        {

            var approvedScenario = (from s in _context.TblSysScenarios
                                    where s.ScenarioStatus == ProcessFilter
                                    where s.ScenarioCreationDate.Value.Year.ToString() == YearFilter || YearFilter == null || YearFilter == ""


                                    group s by new
                                    {
                                        s.ProjectOwner,


                                    } into scenarioByProject
                                    select new
                                    {
                                        ProjectName = scenarioByProject.Key.ProjectOwner,
                                        total = scenarioByProject.Where(c => c.ScenarioCreationDate.Value.Year == DateTime.Today.Year).Count(),

                                    } into prj

                                    select prj).ToList();
            approvedScenario = approvedScenario.OrderBy(x => x.ProjectName).ToList();

            var ExternalProjectData = _externalApprovedScenarioData.GetExternalApprovedScenarioForDashboard(YearFilter);

            foreach (var project in approvedScenario.ToList())

            {
                _dashboardModel.ApprovedScenariosDataForDashboard.Add(new ExecutionLogFieldsForDashboard
                {
                    //Projectid = Guid.NewGuid(),
                    Project = project.ProjectName,
                    Year = YearFilter,
                    ProjectTotal = Convert.ToInt32(project.total),
                });

            }

            foreach (var project in ExternalProjectData)
            {
                _dashboardModel.ApprovedScenariosDataForDashboard.Add(new ExecutionLogFieldsForDashboard
                {

                    Project = project.Project,
                    Year = project.Year,
                    ProjectTotal = project.ProjectTotal,
                });
            }
            return _dashboardModel.ApprovedScenariosDataForDashboard;

        }

        public List<ExecutionLogFieldsForDashboard> GetApprovedScenarioDataByWeekForDashboardChart(string ProcessFilter, string YearFilter)
        {

            var approvedScenario = (from s in _context.TblSysScenarios
                                    let dt = s.ScenarioCreationDate
                                    where s.ScenarioStatus == "Approved"
                                    where s.ScenarioCreationDate.Value.Year.ToString() == DateTime.Today.Year.ToString()
                                    group s by s.ScenarioCreationDate.Value.Date into scenarioByProject
                                    //group s by DateTimeFrom.Date(s.ScenarioCreationDate) into scenarioByProject




                                    select new
                                    {

                                        date = scenarioByProject.Key,
                                        total = scenarioByProject.Count(),

                                    } into prj
                                    select prj).ToList();


            //approvedScenario = approvedScenario.OrderBy(x => x.date.).ToList();
            var ExternalProjectData = _externalApprovedScenarioData.GetExternalApprovedScenarioByWeekForDashboards(YearFilter);

            foreach (var project in approvedScenario.ToList())

            {

                int WeekNum = GetWeekNumber(project.date, CultureInfo.CurrentCulture);

                _dashboardModel.ApprovedScenariosByWeekDataForDashboard.Add(new ExecutionLogFieldsForDashboard
                {
                    NumberOfWeek = WeekNum.ToString(),
                    DateOfTotal = project.date,
                    Year = YearFilter,
                    ProjectTotal = Convert.ToInt32(project.total),
                });

            }

            foreach (var item in ExternalProjectData)
            {
                
                _dashboardModel.ApprovedScenariosByWeekDataForDashboard.Add(new ExecutionLogFieldsForDashboard
                {

                    NumberOfWeek = item.NumberOfWeek,
                    DateOfTotal = item.DateOfTotal,
                    Year = YearFilter,
                    ProjectTotal = item.ProjectTotal,
                });
            }

            return _dashboardModel.ApprovedScenariosByWeekDataForDashboard;

        }

        private static int GetWeekNumber(DateTime date, CultureInfo culture)
        {
            return culture.Calendar.GetWeekOfYear(date,
                culture.DateTimeFormat.CalendarWeekRule,
                culture.DateTimeFormat.FirstDayOfWeek);
        }
    }


}
