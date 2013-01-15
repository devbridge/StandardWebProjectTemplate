using System.Threading;
using System.Web.Mvc;
using DevBridge.Templates.WebProject.Web.Logic.Models.Account;

namespace DevBridge.Templates.WebProject.Web.Controllers
{
    public partial class AccountController : Controller
    {
        public virtual ActionResult LoginStatus()
        {
            var model = new LoginStatusViewModel
                            {
                                IsUserAuthenticated = false
                            };

            return View(MVC.Account.Views.LoginStatus, model);
        }

        public virtual ActionResult Login(LoginViewModel model)
        {
            // Simulate slow connection:
            Thread.Sleep(1000);

            return Json(
                new
                    {
                        success = false,
                        redirectUrl = Url.Action(MVC.Home.Index()),
                        message = "Login is not implemented."
                    },
                JsonRequestBehavior.AllowGet);
        }
    }
}
