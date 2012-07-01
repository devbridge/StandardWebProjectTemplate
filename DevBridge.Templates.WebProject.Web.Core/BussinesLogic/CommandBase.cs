using System.Collections.Generic;
using DevBridge.Templates.WebProject.Common.Mvc;

namespace DevBridge.Templates.WebProject.Web.Core.BussinesLogic
{
	public abstract class CommandBase : ICommand
	{
		protected bool UseManualTransaction { get; set; }
		protected ICollection<ErrorInfo> Errors { get; private set; }

		protected CommandBase()
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