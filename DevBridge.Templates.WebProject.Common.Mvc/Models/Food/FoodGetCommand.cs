using DevBridge.Templates.WebProject.Web.Core.BussinesLogic;

namespace DevBridge.Templates.WebProject.Common.Mvc.Models.Food
{
	public class FoodGetCommand : CommandBase
	{
		public int FoodId { get; set; }
		
		private readonly IRepository repository;
		public FoodViewModel Model;

		public FoodGetCommand(IRepository repository)
		{
			this.repository = repository;
			Model = new FoodViewModel();
		}

		protected override void ExecuteCommand()
		{
			//try get data from DB
			//handle errors
			//do mappings

			Model.Id = 1;
			Model.Name = "apple";
		}
	}
}