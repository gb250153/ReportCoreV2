using ReportCoreV2.DataRepository;
using ReportCoreV2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2.BusinessDataHandler
{
    public class ExternalProjectDataHandler : IExternalProjectDataHandler
    {
       
        private IExternalDataAddViewModel _externalDataAddViewModel;
        private IExtenalData _extenalData;
        public ExternalProjectDataHandler(IExternalExecutionData externalExecutionData, IExternalDataAddViewModel externalDataAddViewModel, IExtenalData extenalData)
        {
            
            _externalDataAddViewModel = externalDataAddViewModel;
            _extenalData = extenalData;
        }
        public IExternalDataAddViewModel GetExternalProjectList()
        {


            _externalDataAddViewModel.ProjectsList = _extenalData.GetExternalProjectList();

            return _externalDataAddViewModel;

        }

    }
}
