using System.Web.Mvc;
using AutoMapper;
using DevBridge.Templates.WebProject.Common.Mvc;
using DevBridge.Templates.WebProject.Common.SampleContracts;
using DevBridge.Templates.WebProject.Common.SampleEntities;
using DevBridge.Templates.WebProject.Common.SampleModels;
using ControllerBase = DevBridge.Templates.WebProject.Common.Mvc.ControllerBase;

namespace DevBridge.Templates.WebProject.Common.SampleService
{
	public class SampleFoodController : ControllerBase
	{
		private readonly IFoodGetService foodGetService;

		public SampleFoodController(IFoodGetService foodGetService)
		{
			this.foodGetService = foodGetService;
		}

		[HttpGet]
		public ActionResult Index(int id)
		{
			var model = Mapper.Map<FoodEntity, FoodViewModel>(foodGetService.GetFood(id));
			return View(model);
		}

		[HttpGet]
		public ActionResult IndexComplexMapping(int id)
		{
			var model = new FoodViewModel().MapComplexModel(foodGetService, id);
			return View(model);
		}

		[HttpPost]
		public ActionResult Index(FoodViewModel model)
		{
			return Service<FoodUpdateService>(
				s => s.Model = Mapper.Map<FoodViewModel, FoodEntity>(model),
				s => View(),
				() => View(model)
			);
		}

		[HttpPost]
		public ActionResult IndexComplexMapping(FoodViewModel model)
		{
			return Service<FoodUpdateService>(
				s => s.Model = model.MapComplexModel(model),
				s => View(),
				() => View(model)
			);
		}

		[HttpPost]
		public ActionResult IndexSaveDataJsonResult(FoodViewModel model)
		{
			var result = Service<FoodUpdateService>(
				cmd => cmd.Model = Mapper.Map<FoodViewModel, FoodEntity>(model),
				cmd => Json(1),
				() => Json(ModelState.GetErrorList()));

			return result;
		}
	}
}