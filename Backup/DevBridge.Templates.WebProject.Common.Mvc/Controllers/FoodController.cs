using System.Web.Mvc;
using DevBridge.Templates.WebProject.Common.Mvc.Infrastructure;
using DevBridge.Templates.WebProject.Common.Mvc.Infrastructure.Extensions;
using DevBridge.Templates.WebProject.Common.Mvc.Models.Food;
using AutoMapper;

namespace DevBridge.Templates.WebProject.Common.Mvc.Controllers
{
	public class SampleFoodController : BaseController
	{
		[HttpGet]
		public ActionResult Index(int id)
		{
			return Command<FoodGetCommand>(
				s => s.FoodId = id,
				s => View(s.Model),
				() => View(new FoodViewModel())
			);
		}
		
		[HttpPost]
		public ActionResult Index(FoodViewModel model)
		{
			return Command<FoodUpdateCommand>(
				s => s.Model = Mapper.Map<FoodViewModel, FoodEntity>(model),
				s => View(),
				() => View(model)
			);
		}

		[HttpPost]
		public ActionResult IndexComplexMapping(FoodViewModel model)
		{
			return Command<FoodUpdateCommand>(
				s => s.Model = model,
				s => View(),
				() => View(model)
			);
		}

		[HttpPost]
		public ActionResult IndexSaveDataJsonResult(FoodViewModel model)
		{
			var result = Command<FoodUpdateCommand>(
				cmd => cmd.Model = Mapper.Map<FoodViewModel, FoodEntity>(model),
				cmd => Json(1),
				() => Json(ModelState.GetErrorList()));

			return result;
		}
	}
}