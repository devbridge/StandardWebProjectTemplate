using DevBridge.Templates.WebProject.Common.Mvc;
using DevBridge.Templates.WebProject.Common.SampleContracts;
using DevBridge.Templates.WebProject.Common.SampleEntities;

namespace DevBridge.Templates.WebProject.Common.SampleService
{
	public class FoodUpdateService : ServiceBase
	{
		private readonly IRepository repository;
		public FoodEntity Model { get; set; }

		public FoodUpdateService(IRepository repository)
		{
			this.repository = repository;
			UseManualTransaction = true;
		}

		protected override void ExecuteCommand()
		{
			if (Model.Name == "apple")
				throw new RulesException("apple are not allowed");

			if (Model.Name == "orange")
			{
				AddError("Orange are not allowed");
				AddError("Orange are not allowed 2");
				throw new RulesException(Errors);
			}

			//some update executon logic
		}
	}

	public class FoodGetService : IFoodGetService
	{
		private readonly IRepository repository;

		public FoodGetService(IRepository repository)
		{
			this.repository = repository;
		}

		public FoodEntity GetFood(int id)
		{
			//get from repository
			return new FoodEntity();
		}
	}
}