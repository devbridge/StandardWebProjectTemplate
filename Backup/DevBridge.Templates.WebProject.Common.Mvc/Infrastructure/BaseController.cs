using System;
using System.Web.Mvc;
using DevBridge.Templates.WebProject.Common.Mvc.Infrastructure.Extensions;

namespace DevBridge.Templates.WebProject.Common.Mvc.Infrastructure
{
    public abstract class BaseController : Controller
    {
		public ActionResult Command<TCommand>(
			Action<TCommand> init,
			Func<TCommand, ActionResult> success,
			Func<ActionResult> failure)
			where TCommand : ICommand
		{
			if (!ModelState.IsValid)
			{
				return failure();
			}

			try
			{
				var service = CommandFactory.GetService<TCommand>();
				init(service);
				service.Execute();
				return success(service);
			}
			catch(RulesException e)
			{
				ModelState.AddModelErrors(e.Errors);
				return failure();
			}
		}

		protected override void HandleUnknownAction(string actionName)
		{
			//redirect to home index on unknown action
			ViewBag.UnknownAction = true;
			//RedirectToAction("Index");
			//base.HandleUnknownAction(actionName);
		}
    }
}
