using System;
using System.Web.Mvc;
using DevBridge.Templates.WebProject.ServiceContracts;
using DevBridge.Templates.WebProject.Tools;
using DevBridge.Templates.WebProject.Web.Helpers;
using DevBridge.Templates.WebProject.Web.Logic.Commands.Agreement.CreateAgreement;
using DevBridge.Templates.WebProject.Web.Logic.Commands.Agreement.GetAgreements;
using Microsoft.Practices.Unity;

namespace DevBridge.Templates.WebProject.Web.Controllers
{
    public partial class AgreementController : Tools.Mvc.ExtendedControllerBase
    {
        [Dependency]
        public IAgreementManagementService AgreementManagementService { get; set; }

        public virtual ActionResult List(GetAgreementsFilter filter)
        {
            var model = GetCommand<GetAgreementsCommand>().ExecuteCommand(filter);            
            return View(model);
        }        

        [HttpPost]
        public virtual ActionResult Create(CreateAgreementCommand createAgreement)
        {
            return Content("TODO: create");
        }
    }
}
