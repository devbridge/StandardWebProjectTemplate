using AutoMapper;
using DevBridge.Templates.WebProject.Common.SampleContracts;
using DevBridge.Templates.WebProject.Common.SampleEntities;

namespace DevBridge.Templates.WebProject.Common.SampleModels
{
	public class FoodViewModel
	{
		public int Id { get; set; } 
		public string Name { get; set; } 

		public FoodEntity MapComplexModel(FoodViewModel model)
		{
			var entity = Mapper.Map<FoodViewModel, FoodEntity>(model);
			entity.Name = string.Format("{0} {1}", entity.Name, model.Id > 1 ? "one" : "two");

			return entity;
		}

		public FoodViewModel MapComplexModel(IFoodGetService foodGetService, int id)
		{
			var model = Mapper.Map<FoodEntity, FoodViewModel>(foodGetService.GetFood(id));
			model.Name = string.Format("{0} {1}", model.Name, model.Id > 1 ? "one" : "two");

			return model;
		}
	}
}