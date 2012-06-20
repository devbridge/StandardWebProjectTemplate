using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace DevBridge.Templates.WebProject.Common.Mvc
{
	public static class ModelStateExtensions
	{
		public static void AddModelErrors(this ModelStateDictionary dictionary, IEnumerable<ErrorInfo> errors)
		{
			foreach (var errorInfo in errors)
			{
		 		dictionary.AddModelError(errorInfo.PropertyName, errorInfo.ErrorMessage);
			}
		}

		public static ErrorInfo[] GetErrorList(this ModelStateDictionary dictionary)
		{
			return dictionary
				.SelectMany(c =>
				            c.Value.Errors.Select(t => new
				                                       	{
				                                       		Property = c.Key,
				                                       		t.ErrorMessage
				                                       	})
				)
				.Select(c => new ErrorInfo(c.Property, c.ErrorMessage)).ToArray();
		}
	}
}