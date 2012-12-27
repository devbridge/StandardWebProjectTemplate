using System.Web.Mvc;
using DevBridge.Templates.WebProject.Web.Logic.Models.Presentation;

namespace DevBridge.Templates.WebProject.Web.Controllers
{
    public partial class PresentationController : Controller
    {
        public virtual ActionResult LoginStatus()
        {
            var model = new LoginStatusViewModel
                            {
                                IsUserAuthenticated = false
                            };

            return View(MVC.Presentation.Views.LoginStatus, model);
        }
    }
}
