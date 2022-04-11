using Microsoft.AspNetCore.Mvc;
using ReportCoreV2.ExternalEfModel;
using ReportCoreV2.Models;
using ReportCoreV2.Models.ModelInterfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.DataRepository
{
    public class ExtenalData : IExtenalData
    {
        private readonly ExternalReportsContext _context;
        private IExternalProjectModel _externalProjectModel;



        public ExtenalData(ExternalReportsContext context, IExternalProjectModel externalProjectModel)
        {
            _context = context;
            _externalProjectModel = externalProjectModel;

        }

        public List<DataListOfExternalProjects> GetExternalProjectList()
        {



            var executionProjects = (from s in _context.Projects

                                     select new
                                     {
                                         projectname = s.ProjectName,
                                     } into d

                                     select d).Distinct();



            executionProjects = executionProjects.OrderBy(x => x.projectname);
            foreach (var project in executionProjects.ToList())
            {

                _externalProjectModel.ExternalProjectList.Add(new DataListOfExternalProjects
                {
                    ProjectName = project.projectname,

                });


            }
            return _externalProjectModel.ExternalProjectList;




        }
        public void AddNewExecution(IExternalAddExecution externalAdd)
        {
            var manualExecution = new ManualExecution();

            manualExecution.ProjectName = externalAdd.ProjectName;
            manualExecution.EntryDate = externalAdd.EntryDate;
            manualExecution.ScenarioExecutionAmount = externalAdd.ScenarioExecutionAmount;

          

            _context.ManualExecutions.Add(manualExecution);
            _context.SaveChanges();



             UpdateProjectExecutionTable();
        }
        private void UpdateProjectExecutionTable()
        {
            MonthlyDataAggregate Sumall = new MonthlyDataAggregate();
            List< ManualProjectsExecution > manualProjectsList = new List<ManualProjectsExecution>();
            if (_context.ManualProjectsExecutions !=null)
            {
                _context.RemoveRange(_context.ManualProjectsExecutions);
                _context.SaveChanges();
            }
                       

            var manualExecution = (from tbl in _context.ManualExecutions
                                   group tbl by new
                                   {
                                       tbl.ProjectName,
                                       tbl.EntryDate.Year,
                                       tbl.EntryDate.Month,


                                   } into executionByProject
                                   //select executionByProject
                                   select new
                                   {
                                       ProjectName = executionByProject.Key.ProjectName,
                                       year = executionByProject.Key.Year,
                                       jan = executionByProject.Where(c => c.EntryDate.Month == 1).Sum(i => i.ScenarioExecutionAmount),
                                       feb = executionByProject.Where(c => c.EntryDate.Month == 2).Sum(i => i.ScenarioExecutionAmount),
                                       mar = executionByProject.Where(c => c.EntryDate.Month == 3).Sum(i => i.ScenarioExecutionAmount),
                                       apr = executionByProject.Where(c => c.EntryDate.Month == 4).Sum(i => i.ScenarioExecutionAmount),
                                       may = executionByProject.Where(c => c.EntryDate.Month == 5).Sum(i => i.ScenarioExecutionAmount),
                                       jun = executionByProject.Where(c => c.EntryDate.Month == 6).Sum(i => i.ScenarioExecutionAmount),
                                       jul = executionByProject.Where(c => c.EntryDate.Month == 7).Sum(i => i.ScenarioExecutionAmount),
                                       aug = executionByProject.Where(c => c.EntryDate.Month == 8).Sum(i => i.ScenarioExecutionAmount),
                                       spt = executionByProject.Where(c => c.EntryDate.Month == 9).Sum(i => i.ScenarioExecutionAmount),
                                       oct = executionByProject.Where(c => c.EntryDate.Month == 10).Sum(i => i.ScenarioExecutionAmount),
                                       nov = executionByProject.Where(c => c.EntryDate.Month == 11).Sum(i => i.ScenarioExecutionAmount),
                                       dec = executionByProject.Where(c => c.EntryDate.Month == 12).Sum(i => i.ScenarioExecutionAmount),


                                   } into prj
                                   select prj


                             ).ToList();

            var groupedList = from p in manualExecution
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


                manualProjectsList.Add(new ManualProjectsExecution
                {
                    
                    Project = project.Key.ProjectName,
                    Year = project.Key.year,
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

            _context.ManualProjectsExecutions.AddRange(manualProjectsList);
            _context.SaveChanges();


        }

        public void AddNewCreatedScenarios(IExternalAddCreatedScenarios externalAddCreatedScenarios)
        {
            var manualScenarioCreation = new ManualScenarioCreation();
            manualScenarioCreation.ProjectName = externalAddCreatedScenarios.ProjectName;
            manualScenarioCreation.EntryDate = externalAddCreatedScenarios.EntryDate;
            manualScenarioCreation.ScenarioCreatedAmount = externalAddCreatedScenarios.ScenarioCreatedAmount;

            _context.ManualScenarioCreations.Add(manualScenarioCreation);
            _context.SaveChanges();
            UpdateProjectScenarioCreationTable();
        }

        private void UpdateProjectScenarioCreationTable()
        {
            MonthlyDataAggregate Sumall = new MonthlyDataAggregate();
            List<ManualProjectsCreatedScenario> manualProjectsList = new List<ManualProjectsCreatedScenario>();
            if (_context.ManualProjectsCreatedScenarios != null)
            {
                _context.RemoveRange(_context.ManualProjectsCreatedScenarios);
                _context.SaveChanges();
            }


            var manualExecution = (from tbl in _context.ManualScenarioCreations
                                   group tbl by new
                                   {
                                       tbl.ProjectName,
                                       tbl.EntryDate.Year,
                                       tbl.EntryDate.Month,


                                   } into executionByProject
                                   //select executionByProject
                                   select new
                                   {
                                       ProjectName = executionByProject.Key.ProjectName,
                                       year = executionByProject.Key.Year,
                                       jan = executionByProject.Where(c => c.EntryDate.Month == 1).Sum(i => i.ScenarioCreatedAmount),
                                       feb = executionByProject.Where(c => c.EntryDate.Month == 2).Sum(i => i.ScenarioCreatedAmount),
                                       mar = executionByProject.Where(c => c.EntryDate.Month == 3).Sum(i => i.ScenarioCreatedAmount),
                                       apr = executionByProject.Where(c => c.EntryDate.Month == 4).Sum(i => i.ScenarioCreatedAmount),
                                       may = executionByProject.Where(c => c.EntryDate.Month == 5).Sum(i => i.ScenarioCreatedAmount),
                                       jun = executionByProject.Where(c => c.EntryDate.Month == 6).Sum(i => i.ScenarioCreatedAmount),
                                       jul = executionByProject.Where(c => c.EntryDate.Month == 7).Sum(i => i.ScenarioCreatedAmount),
                                       aug = executionByProject.Where(c => c.EntryDate.Month == 8).Sum(i => i.ScenarioCreatedAmount),
                                       spt = executionByProject.Where(c => c.EntryDate.Month == 9).Sum(i => i.ScenarioCreatedAmount),
                                       oct = executionByProject.Where(c => c.EntryDate.Month == 10).Sum(i => i.ScenarioCreatedAmount),
                                       nov = executionByProject.Where(c => c.EntryDate.Month == 11).Sum(i => i.ScenarioCreatedAmount),
                                       dec = executionByProject.Where(c => c.EntryDate.Month == 12).Sum(i => i.ScenarioCreatedAmount),


                                   } into prj
                                   select prj


                             ).ToList();

            var groupedList = from p in manualExecution
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


                manualProjectsList.Add(new ManualProjectsCreatedScenario
                {

                    Project = project.Key.ProjectName,
                    Year = project.Key.year,
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

            _context.ManualProjectsCreatedScenarios.AddRange(manualProjectsList);
            _context.SaveChanges();


        }


    }

}
