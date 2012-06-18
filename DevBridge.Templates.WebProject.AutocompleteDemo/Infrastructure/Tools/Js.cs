using System;
using System.Web.Mvc;

namespace DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure.Tools
{
	public static class Js
	{
		public static MvcHtmlString Encode(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return MvcHtmlString.Empty;
			}

			return MvcHtmlString.Create(value
				.Replace("'", "\\'")
				.Replace("\"", "\\\"")
				.Replace("\n", "\\n")
				.Replace("\r", "\\r"));
		}

		public static MvcHtmlString JsonEncode(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return MvcHtmlString.Empty;
			}

			return Encode(value.Replace("\\n", "\\\\n").Replace("\\r", "\\\\r"));
		}

		public static MvcHtmlString ToJsNullable<T>(this T? value) where T : struct
		{
			return MvcHtmlString.Create(value.HasValue ? value.ToString() : "null");
		}

		public static MvcHtmlString ToJsDate(this DateTime? value)
		{
			return value == null ? MvcHtmlString.Create("null") : value.Value.ToJsDate();
		}

		public static MvcHtmlString ToJsDate(this DateTime value)
		{
			return MvcHtmlString.Create(String.Format("new Date({0})", value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds));
		}
	}
}