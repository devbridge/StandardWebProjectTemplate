using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure.Tools
{
	public static class HtmlAttributeHelper
	{
		private const string HtmlAttributes = "HtmlAttributes";

		public static IDictionary<string, object> GetDateAttributes(ViewDataDictionary viewData)
		{
			var attributes = new RouteValueDictionary(viewData[HtmlAttributes]);
			UpdateCssClasses(attributes, "date-picker");
			return attributes;
		}

		public static IDictionary<string, object> GetAutocompleteAttributes(ViewDataDictionary viewData)
		{
			var attributes = new RouteValueDictionary(viewData[HtmlAttributes]);
			UpdateCssClasses(attributes, string.Empty);

			return attributes;
		}

		private static void UpdateCssClasses(IDictionary<string, object> attributes, string mandatoryClasses)
		{
			var cssClasses = new List<string>();
			if (mandatoryClasses != null)
			{
				cssClasses.AddRange(mandatoryClasses.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
			}

			if (attributes.ContainsKey("class"))
			{
				var additionalClasses = ((string)attributes["class"]).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				foreach (var additionalClass in additionalClasses)
				{
					if (!cssClasses.Contains(additionalClass))
					{
						cssClasses.Add(additionalClass);
					}
				}
			}

			if (cssClasses.Any())
			{
				attributes["class"] = cssClasses.Aggregate((x, y) => x + ' ' + y);
			}
		}
	}
}