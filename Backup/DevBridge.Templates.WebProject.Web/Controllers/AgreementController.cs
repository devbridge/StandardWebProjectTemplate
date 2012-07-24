using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevBridge.Templates.WebProject.ServiceContracts;
using DevBridge.Templates.WebProject.Web.Models.AgreementViewModels;
using Microsoft.Practices.Unity;

namespace DevBridge.Templates.WebProject.Web.Controllers
{
    public class AgreementController : Controller
    {
        [Dependency]
        public IAgreementManagementService AgreementManagementService { get; set; }

        public ActionResult List()
        {
            AgreementListViewModel model = new AgreementListViewModel();
            model.Agreements = AgreementManagementService.GetAgreements();
            return View(model);
        }        
    }
}
