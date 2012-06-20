using System;
using System.Collections.Generic;

namespace DevBridge.Templates.WebProject.Common.Mvc
{
	[Serializable]
	public class RulesException : Exception
	{
		public IEnumerable<ErrorInfo> Errors { get; set; } 

		public RulesException()
		{
			Errors = new List<ErrorInfo>();
		}

		public RulesException(IEnumerable<ErrorInfo> errors)
		{
			Errors = errors;
		}

		public RulesException(string errorMessage)
			: this(string.Empty, errorMessage)
		{
		}

		public RulesException(string propertyName, string errorMessage)
		{
			Errors = new[] {new ErrorInfo(propertyName, errorMessage)};
		}
	}
}