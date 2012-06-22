using DevBridge.Templates.WebProject.Common.SampleEntities;

namespace DevBridge.Templates.WebProject.Common.SampleContracts
{
	public interface IFoodGetService
	{
		FoodEntity GetFood(int id);
	}
}