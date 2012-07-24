using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace DevBridge.Templates.WebProject.Common.Mvc.Infrastructure.Helpers
{
	public static class HtmlAttributeHelper
	{
		public const string StringLength = "StringLength";
		private const string HtmlAttributes = "HtmlAttributes";

		public static IDictionary<string, object> GetTextBoxAttributes(ViewDataDictionary viewData)
		{
			var metadata = viewData.ModelMetadata;
			var attributes = new RouteValueDictionary(viewData[HtmlAttributes]);

			object stringLengthValue;
			if (!attributes.ContainsKey("maxlength") && metadata.AdditionalValues.TryGetValue(StringLength, out stringLengthValue))
			{
				attributes.Add("maxlength", stringLengthValue);
			}

			UpdateCssClasses(attributes, "text-box single-line");

			return attributes;
		}

		public static IDictionary<string, object> GetDateAttributes(ViewDataDictionary viewData)
		{
			var attributes = new RouteValueDictionary(viewData[HtmlAttributes]);
			UpdateCssClasses(attributes, "date-picker");

			attributes["data-val"] = "true";
			attributes["data-val-date"] = "Date is incorrect.";
			return attributes;
		}

		public static IDictionary<string, object> GetAutocompleteAttributes(ViewDataDictionary viewData)
		{
			var attributes = new RouteValueDictionary(viewData[HtmlAttributes]);
			UpdateCssClasses(attributes, string.Empty);

			return attributes;
		}

		public static IDictionary<string, object> GetTimeSpanAttributes(ViewDataDictionary viewData)
		{
			var attributes = new RouteValueDictionary(viewData[HtmlAttributes]);
			UpdateCssClasses(attributes, "text-box single-line time-picker");

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