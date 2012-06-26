using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
	public static class EditorExtensions
	{
		public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel,TValue>> expression, object htmlAttributes)
		{
			return html.EditorFor(expression, additionalViewData: new { HtmlAttributes = htmlAttributes});
		}

		public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
		{
			return html.DisplayFor(expression, additionalViewData: new { HtmlAttributes = htmlAttributes });
		}
	}
}