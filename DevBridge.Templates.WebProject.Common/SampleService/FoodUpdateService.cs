using DevBridge.Templates.WebProject.Common.Mvc;

namespace DevBridge.Templates.WebProject.Common.SampleService
{
	public class FoodUpdateService : ServiceBase
	{
		public string SomeUpdateParam { get; set; }

		public FoodUpdateService()
		{
			UseManualTransaction = true;
		}

		protected override void ExecuteCommand()
		{
			if (SomeUpdateParam == "test")
				throw new RulesException("test rules exception");

			if(SomeUpdateParam == "test2")
			{
				AddError("added rules exception 1");
				AddError("property 2", "added rules exception 2");
				throw new RulesException(Errors);
			}

			//some update executon logic
		}
	}
}