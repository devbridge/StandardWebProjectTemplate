using System.Web.Mvc;

using DevBridge.Templates.WebProject.Web.Helpers;
using DevBridge.Templates.WebProject.Web.Logic.Commands.Agreement.CreateAgreement;
using DevBridge.Templates.WebProject.Web.Logic.Commands.Agreement.GetAgreements;
using DevBridge.Templates.WebProject.Web.Logic.Models.Agreement;

namespace DevBridge.Templates.WebProject.Web.Controllers
{
    public partial class AgreementController : Tools.Mvc.ExtendedControllerBase
    {
        public virtual ActionResult List(GetAgreementsFilter filter)
        {
            var model = GetCommand<GetAgreementsCommand>().ExecuteCommand(filter);

            return View(model);
        }        

        [HttpPost]
        public virtual ActionResult Create(CreateAgreementViewModel createAgreement)
        {
            if (ModelState.IsValid)
            {
                var success = GetCommand<CreateAgreementCommand>().ExecuteCommand(createAgreement);

                if (success)
                {
                    Messages.AddSuccess("Success");
                }
                else
                {
                    Messages.AddError("Failed to create.");
                }
            }

            return View();
        }
    }
}
