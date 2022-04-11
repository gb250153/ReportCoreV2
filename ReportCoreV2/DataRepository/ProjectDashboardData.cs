using ReportCoreV2.ATCEfModel;
using ReportCoreV2.Models;
using ReportCoreV2.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.DataRepository
{
    public class ProjectDashboardData : IProjectDashboardData
    {
        private readonly ATCContext _context;
        private IProjectDataModel _projectDataModel;
        private IProjectDashboardModel _projectDashboardModel;
        private IExternalExecutionData _externalExecutionData;


        public ProjectDashboardData(ATCContext context, IProjectDashboardModel projectDashboardModel, IExternalExecutionData externalExecutionData, IProjectDataModel projectDataModel)
        {
            _context = context;
            _projectDataModel = projectDataModel;
            _projectDashboardModel = projectDashboardModel;
            _externalExecutionData = externalExecutionData;
        }

        public List<CycleResultFieldsForProjectDashboard> GetCycleDataForProjectDashboard(string ProjectFilter)
        {

            var lastExecutionDate = (from o in _context.TblSysExecutionLogs
                                     where o.Project == ProjectFilter
                                     where o.TestSetCycle != null
                                     group o by o.Project into g
                                     select g.Max(t => t.DateReq))

                                    .ToList();

            if (lastExecutionDate.Count != 0)
            {
                DateTime LatestCycleDate = lastExecutionDate.FirstOrDefault().Date;
                DateTime FollowingLatestCycleDate = LatestCycleDate.AddDays(1);
                var cycleresult = (from s in _context.TblSysExecutionLogs
                                   where s.TestSetCycle != null
                                   where s.ScenarioCounter != 0
                                   where s.Project == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                   where s.DateReq >= LatestCycleDate
                                   where s.DateReq <= FollowingLatestCycleDate


                                   group s by new
                                   {
                                       s.Project,
                                       s.TestSetCycle,
                                       s.DateReq,


                                   }


                            into executionByProject
                                   select new
                                   {

                                       ProjectName = executionByProject.Key.Project,
                                       cyclename = executionByProject.Key.TestSetCycle,
                                       dateReq = executionByProject.Key.DateReq,
                                       setcounter = executionByProject.Where(c => c.TestSetName == c.TestSetName).Count(),
                                       scenariocount = executionByProject.Sum(i => i.ScenarioCounter),
                                       scenarioexecutecount = executionByProject.Sum(i => i.ScenarioCounterExecuted),
                                       scenariopasscount = executionByProject.Sum(i => i.ScenarioCounterExecutedPass),
                                       scenariofailcount = executionByProject.Sum(i => i.ScenarioCounterExecutedFail),
                                       scenarioFerrorcount = executionByProject.Sum(i => i.ScenarioCounterExecutedFError),
                                       scenarioTerrorcount = executionByProject.Sum(i => i.ScenarioCounterExecutedTError),


                                   } into prj

                                   select prj




                         ).ToList();

                foreach (var item in cycleresult.ToList())

                {
                    _projectDashboardModel.CycleResultDataForProjectDashboard.Add(new CycleResultFieldsForProjectDashboard

                    {
                        Cycleid = Guid.NewGuid(),
                        Project = item.ProjectName,
                        CycleName = item.cyclename,
                        Timestemp = item.dateReq.ToString(),
                        SetCountInCycle = item.setcounter,
                        ScenarioCountInCycle = Convert.ToInt32(item.scenariocount),
                        ScenarioExecutedInCycle = Convert.ToInt32(item.scenarioexecutecount),
                        PassScenario = Convert.ToInt32(item.scenariopasscount),
                        FailScenario = Convert.ToInt32(item.scenariofailcount),
                        FlowErrorScenario = Convert.ToInt32(item.scenarioFerrorcount),
                        TechErrorScenario = Convert.ToInt32(item.scenarioTerrorcount),

                    });
                    return _projectDashboardModel.CycleResultDataForProjectDashboard;
                }

            }

            _projectDashboardModel.NoDataMessage = "No Data to Display";



            return _projectDashboardModel.CycleResultDataForProjectDashboard;


        }

        public List<DataListOfProjects> GetProjectsListForDropdown()
        {


            var executionProjects = (from s in _context.TblSysExecutionLogs

                                     select new
                                     {
                                         projectname = s.Project,
                                     } into d

                                     select d).Distinct();

            var archiveProjects = (from s in _context.TblSysExecutionLogArchives

                                   select new
                                   {
                                       projectname = s.Project,

                                   } into d

                                   select d).Distinct();

            var projects = executionProjects.Union(archiveProjects);

            projects = projects.OrderBy(x => x.projectname);
            foreach (var project in projects.ToList())
            {

                _projectDataModel.ProjectList.Add(new DataListOfProjects
                {
                    Project = project.projectname,

                });


            }
            return _projectDataModel.ProjectList;



        }

        public IProjectDashboardModel GetProjectCreatedByList(string ProjectFilter)
        {
            var totalCreatedScenario = (from s in _context.TblSysScenarios
                                        where s.ProjectOwner == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        where s.ScenarioStatus == "Approved"
                                        where s.ScenarioCreationDate.Value.Year == DateTime.Today.Year
                                        
                                        group s by new
                                        {
                                            s.ScenarioCreateBy,
                                           

                                        }

                                    into createdByProject
                                        select new
                                        {

                                            Creator = createdByProject.Key.ScenarioCreateBy,
                                            scenariocreatedcount = createdByProject.Where(c=> c.ScenarioCreateBy == createdByProject.Key.ScenarioCreateBy).Count(),
                                            LastCreated = createdByProject.Max(c=> c.ScenarioCreationDate)

                                        } into prj

                                        select prj).ToList();

            if (totalCreatedScenario.Count != 0)
            {
                foreach (var item in totalCreatedScenario)
                {
                    _projectDashboardModel.ProjectScenarioCreatedBy.Add(new ProjectScenarioCreatedBy
                    {
                        Name = item.Creator,
                        AmountCreated = item.scenariocreatedcount.ToString(),
                        LastCreated = item.LastCreated.Value.Date,
                    });

                    
                }

            }
            else
            {
                _projectDashboardModel.NoDataMessage = "No Data To Display";

                //_projectDashboardModel.ProjectScenarioCreatedBy.Add(new ProjectScenarioCreatedBy
                //{
                //    Name = "",
                //    AmountCreated = "",
                //   //LastCreated = ,
                //});
            }

            return _projectDashboardModel;


        }

        public IProjectDashboardModel GetLastYearCurrentMonthTotalScenarioExecution(string ProjectFilter)
        {
            var LastYear = DateTime.Today.AddYears(-1);
            var firstDay = new DateTime(LastYear.Year, LastYear.Month, 1);
            var lastDay = LastYear;

            var totalExecuteScenario = (from s in _context.TblSysExecutionLogs
                                        where s.Project == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        where s.Process == "Completed"
                                        where s.DateReq.Year == LastYear.Year
                                        where s.DateReq.Month == DateTime.Today.Month
                                        group s by new
                                        {
                                            s.Project,

                                        }

                                    into executionByProject
                                        select new
                                        {
                                            scenarioexecutecount = executionByProject.Where(c => c.DateReq >= firstDay && c.DateReq <= lastDay).Sum(i => i.ScenarioCounterExecuted),

                                        } into prj

                                        select prj).ToList();

            if (totalExecuteScenario.Count != 0)
            {
                foreach (var item in totalExecuteScenario)
                {
                    _projectDashboardModel.TotalLastYearExecutedScenarioInCurrentMonth = item.scenarioexecutecount.ToString();
                }

            }
            else
            {

                _projectDashboardModel.TotalLastYearExecutedScenarioInCurrentMonth = "0";
            }


            return _projectDashboardModel;
        }

        public IProjectDashboardModel GetCurrentMonthTotalScenarioExecution(string ProjectFilter)
        {
            var totalExecuteScenario = (from s in _context.TblSysExecutionLogs
                                        where s.Project == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        where s.Process == "Completed"
                                        where s.DateReq.Year == DateTime.Today.Year
                                        where s.DateReq.Month == DateTime.Today.Month
                                        group s by new
                                        {
                                            s.Project,
                                            
                                        }

                                    into executionByProject
                                        select new
                                        {
                                            scenarioexecutecount = executionByProject.Where(c => c.DateReq.Month == DateTime.Today.Month).Sum(i => i.ScenarioCounterExecuted),

                                        } into prj

                                        select prj).ToList();

            if (totalExecuteScenario.Count != 0)
            {
                foreach (var item in totalExecuteScenario)
                {
                    _projectDashboardModel.TotalExecutedScenarioInCurrentMonth = item.scenarioexecutecount.ToString();
                }

            }
            else 
            {

                _projectDashboardModel.TotalExecutedScenarioInCurrentMonth = "0";
            }
                
  
            return _projectDashboardModel;
        }
        public IProjectDashboardModel GetLastYearTotalScenarioExecution(string ProjectFilter)
        {
            var LastYear = DateTime.Today.AddYears(-1);
            var firstDay = new DateTime(LastYear.Year, 1, 1);
            var lastDay = LastYear;

            var totalExecuteScenario = (from s in _context.TblSysExecutionLogs
                                        where s.Project == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        where s.Process == "Completed"
                                        where s.DateReq.Year == LastYear.Year
                                        group s by new
                                        {
                                            s.Project,

                                        }

                                    into executionByProject
                                        select new
                                        {
                                            scenarioexecutecount = executionByProject.Where(c => c.DateReq >= firstDay && c.DateReq <= lastDay).Sum(i => i.ScenarioCounterExecuted),

                                        } into prj

                                        select prj).ToList();

            var totalArchivedExecuteScenario = (from s in _context.TblSysExecutionLogArchives
                                        where s.Project == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        where s.Process == "Completed"
                                        where s.DateReq.Year == LastYear.Year
                                        group s by new
                                        {
                                            s.Project,

                                        }

                                    into executionByProject
                                        select new
                                        {
                                            scenarioexecutecount = executionByProject.Where(c => c.DateReq >= firstDay && c.DateReq <= lastDay).Sum(i => i.ScenarioCounterExecuted),

                                        } into prj

                                        select prj).ToList();

            var joinedList = totalExecuteScenario.Union(totalArchivedExecuteScenario).Distinct().ToList();
            int totalScenarioExecuted = 0;
            

            if (joinedList.Count != 0)
            {
                foreach (var item in joinedList)
                {
                    totalScenarioExecuted += Convert.ToInt32(item.scenarioexecutecount);
                }
                _projectDashboardModel.TotalExecutedScenarioInLastYear = totalScenarioExecuted.ToString();
            }
            else
            {

                _projectDashboardModel.TotalExecutedScenarioInLastYear = "0";
            }


            return _projectDashboardModel;
        }

        public IProjectDashboardModel GetCurrentYearTotalScenarioExecution(string ProjectFilter)
        {
            var totalExecuteScenario = (from s in _context.TblSysExecutionLogs
                                        where s.Project == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        where s.Process == "Completed"
                                        where s.DateReq.Year == DateTime.Today.Year
                                        group s by new
                                        {
                                            s.Project,

                                        }

                                    into executionByProject
                                        select new
                                        {
                                            scenarioexecutecount = executionByProject.Where(c => c.DateReq.Year == DateTime.Today.Year).Sum(i => i.ScenarioCounterExecuted),

                                        } into prj

                                        select prj).ToList();

            if (totalExecuteScenario.Count != 0)
            {
                foreach (var item in totalExecuteScenario)
                {
                    _projectDashboardModel.TotalExecutedScenarioInCurrentYear = item.scenarioexecutecount.ToString();
                }

            }
            else
            {

                _projectDashboardModel.TotalExecutedScenarioInCurrentYear = "0";
            }

         
            return _projectDashboardModel;
        }

        public IProjectDashboardModel GetLastYearTotalScenarioCreated(string ProjectFilter)
        {
           
            var LastYear = DateTime.Today.AddYears(-1);
            var firstDay =  new DateTime(LastYear.Year, 1, 1);
            var lastDay = LastYear;

            var totalCreatedScenario = (from s in _context.TblSysScenarios
                                        where s.ProjectOwner == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        where s.ScenarioStatus == "Approved"
                                        where s.ScenarioCreationDate.Value.Year == LastYear.Year
                                        group s by new
                                        {
                                            s.ProjectOwner,

                                        }

                                    into createdByProject
                                        select new
                                        {

                                            scenariocreatedcount = createdByProject.Where(c=> c.ScenarioCreationDate >= firstDay && c.ScenarioCreationDate <= lastDay).Count(),

                                        } into prj

                                        select prj).ToList();
            if (totalCreatedScenario.Count != 0)
            {
                foreach (var item in totalCreatedScenario)
                {
                    _projectDashboardModel.TotalCreatedScenarioInLastYear = item.scenariocreatedcount.ToString();
                }

            }
            else
            {

                _projectDashboardModel.TotalCreatedScenarioInLastYear = "0";
            }

            return _projectDashboardModel;
        }
        public IProjectDashboardModel GetCurrentYearTotalScenarioCreated(string ProjectFilter)
        {
            var totalCreatedScenario = (from s in _context.TblSysScenarios
                                        where s.ProjectOwner == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        where s.ScenarioStatus == "Approved"
                                        where s.ScenarioCreationDate.Value.Year == DateTime.Today.Year
                                        group s by new
                                        {
                                            s.ProjectOwner,

                                        }

                                    into createdByProject
                                        select new
                                        {
                                           
                                            scenariocreatedcount = createdByProject.Count(),

                                        } into prj

                                        select prj).ToList();
            if (totalCreatedScenario.Count != 0)
            {
                foreach (var item in totalCreatedScenario)
                {
                    _projectDashboardModel.TotalCreatedScenarioInCurrentYear = item.scenariocreatedcount.ToString();
                }

            }
            else
            {

                _projectDashboardModel.TotalCreatedScenarioInCurrentYear = "0";
            }
            
            return _projectDashboardModel;
        }

        public IProjectDashboardModel GetLastYearCurrentMonthTotalScenarioCreated(string ProjectFilter)
        {
            var LastYear = DateTime.Today.AddYears(-1);
            var firstDay = new DateTime(LastYear.Year, LastYear.Month, 1);
            var lastDay = LastYear;
            var totalCreatedScenario = (from s in _context.TblSysScenarios
                                        where s.ProjectOwner == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        where s.ScenarioStatus == "Approved"
                                        where s.ScenarioCreationDate.Value.Year == LastYear.Year
                                        where s.ScenarioCreationDate.Value.Month == DateTime.Today.Month
                                        group s by new
                                        {
                                            s.ProjectOwner,

                                        }

                                    into createdByProject
                                        select new
                                        {

                                            scenariocreatedcount = createdByProject.Where(c => c.ScenarioCreationDate >= firstDay && c.ScenarioCreationDate <= lastDay).Count(),

                                        } into prj

                                        select prj).ToList();

            if (totalCreatedScenario.Count != 0)
            {
                foreach (var item in totalCreatedScenario)
                {
                    _projectDashboardModel.TotalCreatedScenarioInLastYearCurrentMonth = item.scenariocreatedcount.ToString();
                }

            }
            else
            {

                _projectDashboardModel.TotalCreatedScenarioInLastYearCurrentMonth = "0";
            }

            return _projectDashboardModel;
        }
        public IProjectDashboardModel GetCurrentMonthTotalScenarioCreated(string ProjectFilter)
        {
            var totalCreatedScenario = (from s in _context.TblSysScenarios
                                        where s.ProjectOwner == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        where s.ScenarioStatus == "Approved"
                                        where s.ScenarioCreationDate.Value.Year == DateTime.Today.Year
                                        where s.ScenarioCreationDate.Value.Month == DateTime.Today.Month
                                        group s by new
                                        {
                                            s.ProjectOwner,

                                        }

                                    into createdByProject
                                        select new
                                        {

                                            scenariocreatedcount = createdByProject.Count(),

                                        } into prj

                                        select prj).ToList();

            if (totalCreatedScenario.Count != 0)
            {
                foreach (var item in totalCreatedScenario)
                {
                    _projectDashboardModel.TotalCreatedScenarioInCurrentMonth = item.scenariocreatedcount.ToString();
                }

            }
            else
            {

                _projectDashboardModel.TotalCreatedScenarioInCurrentMonth = "0";
            }
            
            return _projectDashboardModel;
        }

        public IProjectDashboardModel GetLastYearTotalScenarioMaintain(string ProjectFilter)
        {
            var LastYear = DateTime.Today.AddYears(-1);
            var firstDay = new DateTime(LastYear.Year, LastYear.Month, 1);
            var lastDay = LastYear;
            var totalCreatedScenario = (from s in _context.TblSysScenarios
                                        where s.ProjectOwner == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        where s.ScenarioStatus == "Approved"
                                        where s.ScenarioLastUpdate != s.ScenarioCreationDate
                                        where s.ScenarioLastUpdate.Value.Year == LastYear.Year
                                        group s by new
                                        {
                                            s.ProjectOwner,

                                        }

                                    into createdByProject
                                        select new
                                        {

                                            scenariocreatedcount = createdByProject.Where(c => c.ScenarioLastUpdate >= firstDay && c.ScenarioLastUpdate <= lastDay).Count(),

                                        } into prj

                                        select prj).ToList();
            if (totalCreatedScenario.Count != 0)
            {
                foreach (var item in totalCreatedScenario)
                {
                    _projectDashboardModel.TotalMaintainScenarioInLastYear = item.scenariocreatedcount.ToString();
                }

            }
            else
            {

                _projectDashboardModel.TotalMaintainScenarioInLastYear = "0";
            }

            return _projectDashboardModel;
        }
        public IProjectDashboardModel GetCurrentYearTotalScenarioMaintain(string ProjectFilter)
        {
            var totalCreatedScenario = (from s in _context.TblSysScenarios
                                        where s.ProjectOwner == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        where s.ScenarioStatus == "Approved"
                                        where s.ScenarioLastUpdate != s.ScenarioCreationDate
                                        where s.ScenarioLastUpdate.Value.Year == DateTime.Today.Year
                                        group s by new
                                        {
                                            s.ProjectOwner,

                                        }

                                    into createdByProject
                                        select new
                                        {

                                            scenariocreatedcount = createdByProject.Count(),

                                        } into prj

                                        select prj).ToList();
            if (totalCreatedScenario.Count != 0)
            {
                foreach (var item in totalCreatedScenario)
                {
                    _projectDashboardModel.TotalMaintainScenarioInCurrentYear = item.scenariocreatedcount.ToString();
                }

            }
            else
            {

                _projectDashboardModel.TotalMaintainScenarioInCurrentYear = "0";
            }

            return _projectDashboardModel;
        }

        public IProjectDashboardModel GetLastyearCurrentMonthTotalScenarioMaintain(string ProjectFilter)
        {
            var LastYear = DateTime.Today.AddYears(-1);
            var firstDay = new DateTime(LastYear.Year, LastYear.Month, 1);
            var lastDay = LastYear;
            var totalCreatedScenario = (from s in _context.TblSysScenarios
                                        where s.ProjectOwner == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        where s.ScenarioStatus == "Approved"
                                        where s.ScenarioLastUpdate != s.ScenarioCreationDate
                                        where s.ScenarioLastUpdate.Value.Year == LastYear.Year
                                        where s.ScenarioLastUpdate.Value.Month == DateTime.Today.Month
                                        group s by new
                                        {
                                            s.ProjectOwner,

                                        }

                                    into createdByProject
                                        select new
                                        {

                                            scenariocreatedcount = createdByProject.Where(c => c.ScenarioLastUpdate >= firstDay && c.ScenarioLastUpdate <= lastDay).Count(),

                                        } into prj

                                        select prj).ToList();

            if (totalCreatedScenario.Count != 0)
            {
                foreach (var item in totalCreatedScenario)
                {
                    _projectDashboardModel.TotalMaintainScenarioInLastYearCurrentMonth = item.scenariocreatedcount.ToString();
                }

            }
            else
            {

                _projectDashboardModel.TotalMaintainScenarioInLastYearCurrentMonth = "0";
            }

            return _projectDashboardModel;
        }

        public IProjectDashboardModel GetCurrentMonthTotalScenarioMaintain(string ProjectFilter)
        {
            var totalCreatedScenario = (from s in _context.TblSysScenarios
                                        where s.ProjectOwner == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        where s.ScenarioStatus == "Approved"
                                        where s.ScenarioLastUpdate != s.ScenarioCreationDate
                                        where s.ScenarioLastUpdate.Value.Year == DateTime.Today.Year
                                        where s.ScenarioLastUpdate.Value.Month == DateTime.Today.Month
                                        group s by new
                                        {
                                            s.ProjectOwner,

                                        }

                                    into createdByProject
                                        select new
                                        {

                                            scenariocreatedcount = createdByProject.Count(),

                                        } into prj

                                        select prj).ToList();

            if (totalCreatedScenario.Count != 0)
            {
                foreach (var item in totalCreatedScenario)
                {
                    _projectDashboardModel.TotalMaintainScenarioInCurrentMonth = item.scenariocreatedcount.ToString();
                }

            }
            else
            {

                _projectDashboardModel.TotalMaintainScenarioInCurrentMonth = "0";
            }

            return _projectDashboardModel;
        }

        private IProjectDashboardModel GetProjectScenarioType(List<TblSysScenario> ProjectScenarios)
        {
            int Regression = 0;
            int Sanity = 0;
            var scenarioType = (from s in ProjectScenarios
                                group s by new
                                {
                                    s.ScenarioType,

                                }

                                        into createdByProject
                                select new
                                {

                                    Regression = createdByProject.Where(c => c.ScenarioType == "Regression").Count(),
                                    Sanity = createdByProject.Where(c => c.ScenarioType == "Sanity").Count(),


                                } into prj

                                select prj).ToList();
            foreach (var item in scenarioType)
            {
                Regression += item.Regression;
                Sanity += item.Sanity;
                
            }
            _projectDashboardModel.ScenarioTypeRegression = Regression.ToString();
            _projectDashboardModel.ScenarioTypeSanity = Sanity.ToString();

            return _projectDashboardModel;
        }

 
        private IProjectDashboardModel GetProjectScenarioApplink(List<TblSysScenario> ProjectScenarios)
        {
            var ScenarioApplink = (from s in ProjectScenarios
                                   group s by new
                                   {
                                       s.AppLink,

                                   }

                                  into createdByProject
                                   select new
                                   {
                                       Name = createdByProject.Key.AppLink,
                                       Amount = createdByProject.Where(c => c.AppLink == createdByProject.Key.AppLink).Count(),
                                   }
                                  into prj

                                   select prj).ToList();
            var sortedList = ScenarioApplink.OrderBy(c => c.Name);
            foreach (var item in sortedList)
            {
                _projectDashboardModel.projectAppLinks.Add(new ProjectAppLinkInfo

                {
                    Name = item.Name,
                    Amount = item.Amount.ToString(),

                });

            }
            return _projectDashboardModel;
        }
        public IProjectDashboardModel GetProjectTotalScenarios(string ProjectFilter)
        {
            var totalCreatedScenario = (from s in _context.TblSysScenarios
                                        where s.ProjectOwner == ProjectFilter || ProjectFilter == null || ProjectFilter == ""
                                        

                                        select s).ToList();

            var totalScenariosInProject = totalCreatedScenario.Count;

            GetProjectScenarioType(totalCreatedScenario);
            GetProjectScenarioApplink(totalCreatedScenario);

            _projectDashboardModel.TotalScenariosInProject = totalScenariosInProject.ToString();
            

           


            return _projectDashboardModel;

        }
    }
}
