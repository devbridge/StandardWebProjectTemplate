using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DevBridge.Templates.WebProject.Common.Mvc.Infrastructure.Helpers
{
	public static class BoolValueHelper
	{
		public static List<SelectListItem> TriStateValue(string chooseText, string trueText, string falseText, object model)
		{
			var boolValue = GetBoolValue(model);
			return new List<SelectListItem>
			    {
			       	new SelectListItem {Text = chooseText, Value = String.Empty, Selected = !boolValue.HasValue},
			       	new SelectListItem {Text = trueText, Value = "true", Selected = boolValue.HasValue && boolValue.Value},
			       	new SelectListItem {Text = falseText, Value = "false", Selected = boolValue.HasValue && !boolValue.Value}
			    };
		}

		public static bool? GetBoolValue(object model)
		{
			if (model == null)
			{
				return null;
			}

			return Convert.ToBoolean(model, System.Globalization.CultureInfo.InvariantCulture);
		}
	}
}