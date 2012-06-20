using System.Web.Mvc;
using DevBridge.Templates.WebProject.Common.Mvc;
using ControllerBase = DevBridge.Templates.WebProject.Common.Mvc.ControllerBase;

namespace DevBridge.Templates.WebProject.Common.SampleService
{
	public class SampleFoodController : ControllerBase
	{
		[HttpPost]
		public ActionResult Index()
		{
			return Service<FoodUpdateService>(
				s => s.SomeUpdateParam = "test",
				s => View(),
				() => View()
			);
		}

		[HttpPost]
		public ActionResult SaveData(string someModel)
		{
			var result = Service<FoodUpdateService>(
				cmd => cmd.SomeUpdateParam = someModel,
				cmd => Json(1),
				() => Json(ModelState.GetErrorList()));

			return result;
		}
	}
}