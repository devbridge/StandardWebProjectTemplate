using System.Web.Mvc;
using DevBridge.Templates.WebProject.ServiceContracts;
using Microsoft.Practices.Unity;

namespace DevBridge.Templates.WebProject.Web.Controllers
{
    public partial class HomeController : Controller
    {


        public virtual ActionResult Index()
        {
           
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public virtual ActionResult About()
        {
            return View();
        }
    }
}
