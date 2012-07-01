using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using DevBridge.Tools.Helpers;

namespace DevBridge.Tools.Extensions
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