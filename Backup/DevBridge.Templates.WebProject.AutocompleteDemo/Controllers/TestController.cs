using System.Web.Mvc;
using DevBridge.Templates.WebProject.AutocompleteDemo.Models.Test;

namespace DevBridge.Templates.WebProject.AutocompleteDemo.Controllers
{
    public class TestController : Controller
    {
    	public ActionResult Index()
    	{
    		var model = new Test();
			return View(model);
        }

		[HttpPost]
		public ActionResult Index(Test model)
		{
			return View(model);
		}
    }
}
