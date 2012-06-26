using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using DevBridge.Kernel.Web.Mvc.Helpers;

namespace DevBridge.Kernel.Web.Mvc.Extensions
{
	public static class UrlExtensions
	{
		public static MvcHtmlString JsAction<TController>(this UrlHelper url, Expression<Action<TController>> action)
		   where TController : Controller
		{
			var routeValues = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(action);
			return Js.Encode(url.Action(null, routeValues));
		}
	}
}