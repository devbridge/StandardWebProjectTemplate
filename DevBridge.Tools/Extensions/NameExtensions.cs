using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
	public static class NameExtensions
	{
		public static MvcHtmlString Id(this HtmlHelper html, string name)
		{
			var id = html.ViewData.TemplateInfo.GetFullHtmlFieldId(name)
				.Replace('[', '_')
				.Replace(']', '_');
			return MvcHtmlString.Create(html.AttributeEncode(id));
		}

		public static MvcHtmlString IdFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
		{
			return html.Id(ExpressionHelper.GetExpressionText(expression));
		}

		public static MvcHtmlString IdForModel(this HtmlHelper html)
		{
			return html.Id(String.Empty);
		}

		public static MvcHtmlString Name(this HtmlHelper html, string name)
		{
			return MvcHtmlString.Create(html.AttributeEncode(html.ViewData.TemplateInfo.GetFullHtmlFieldName(name)));
		}

		public static MvcHtmlString NameFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
		{
			return html.Name(ExpressionHelper.GetExpressionText(expression));
		}

		public static MvcHtmlString NameForModel(this HtmlHelper html)
		{
			return html.Name(String.Empty);
		}
	} 
}