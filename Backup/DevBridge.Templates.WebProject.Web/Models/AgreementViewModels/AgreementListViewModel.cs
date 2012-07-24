using System.Collections.Generic;
using DevBridge.Templates.WebProject.DataEntities.Entities;

namespace DevBridge.Templates.WebProject.Web.Models.AgreementViewModels
{
    public class AgreementListViewModel
    {
        public IList<Agreement> Agreements { get; set; }
    }
}