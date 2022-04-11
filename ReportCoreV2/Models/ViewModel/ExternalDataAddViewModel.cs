using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.Models.ViewModel
{
    public class ExternalDataAddViewModel : IExternalDataAddViewModel
    {
        List<DataListOfExternalProjects> _projectsData = new List<DataListOfExternalProjects>();
        
        public SelectList ProjectsDatalistAsString
        {
            get
            {
                var listToCOnvert = new List<string>();

                foreach (var item in _projectsData)
                {
                    listToCOnvert.Add(item.ProjectName.ToString());
                }

                return new SelectList(listToCOnvert);
            }
        }




        public List<DataListOfExternalProjects> ProjectsList { get { return _projectsData; } set { _projectsData = value; } }

        
       [Required]
        public string Project { get; set; }
        [Required]
        public DateTime EntryDate { get; set; }
       

        [Required]
        [Range(1, 1000000, ErrorMessage = "ATTN: Illegal amount")]
        public Int32 Amount { get; set; }

    }
}
