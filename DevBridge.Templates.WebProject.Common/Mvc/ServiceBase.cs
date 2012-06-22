using System.Collections.Generic;

namespace DevBridge.Templates.WebProject.Common.Mvc
{
	public abstract class ServiceBase : IService
	{
		protected bool UseManualTransaction { get; set; }
		protected ICollection<ErrorInfo> Errors { get; private set; }

		protected ServiceBase()
		{
			Errors = new List<ErrorInfo>();
		}

		protected void AddError(string errorMessage)
		{
			AddError(string.Empty, errorMessage);
		}

		protected void AddError(string propertyName, string errorMessage)
		{
			Errors.Add(new ErrorInfo(propertyName, errorMessage));
		}

		public void Execute()
		{
			if(UseManualTransaction)
			{
				ExecuteCommand();
				return;
			}

			//using(var tx = new TransactionScope..){
				ExecuteCommand();
				//tx.Commit();
			//}
		}
		
		protected abstract void ExecuteCommand();
	}
}