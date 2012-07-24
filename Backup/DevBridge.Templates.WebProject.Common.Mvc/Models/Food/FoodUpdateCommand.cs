using DevBridge.Templates.WebProject.Web.Core.BussinesLogic;

namespace DevBridge.Templates.WebProject.Common.Mvc.Models.Food
{
	public class FoodUpdateCommand : CommandBase
	{
		private readonly IRepository repository;
		public FoodViewModel Model { get; set; }

		public FoodUpdateCommand(IRepository repository)
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
}