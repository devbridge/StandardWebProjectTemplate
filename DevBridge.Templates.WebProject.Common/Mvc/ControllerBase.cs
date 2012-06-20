using System;
using System.Web.Mvc;

namespace DevBridge.Templates.WebProject.Common.Mvc
{
    public abstract class ControllerBase : Controller
    {
		public ActionResult Service<TService>(
			Action<TService> init,
			Func<TService, ActionResult> success,
			Func<ActionResult> failure)
			where TService : IService
		{
			if (!ModelState.IsValid)
			{
				return failure();
			}

			try
			{
				var service = ServiceFactory.GetService<TService>();
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
    }
}
