using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace ReportCoreV2.Models.ViewModel
{
    public interface IExternalDataAddViewModel
    {
        SelectList ProjectsDatalistAsString { get; }
        List<DataListOfExternalProjects> ProjectsList { get; set; }
        string Project { get; set; }
        DateTime EntryDate { get; set; }
        int Amount { get; set; }
    }
}