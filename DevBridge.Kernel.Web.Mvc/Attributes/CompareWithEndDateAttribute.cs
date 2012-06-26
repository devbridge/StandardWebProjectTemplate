using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DevBridge.Kernel.Web.Mvc.Attributes
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class CompareWithEndDateAttribute : ValidationAttribute, IClientValidatable 
	{
		public string PropertyEndDate {get; private set; }

		public CompareWithEndDateAttribute(string propertyEndDate)
		{
			PropertyEndDate = propertyEndDate;
			ErrorMessage = @"End date has to be later then start date";
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var propertyToMatch = validationContext.ObjectType.GetProperty(PropertyEndDate);
			if (propertyToMatch == null)
			{
				throw new InvalidOperationException(String.Format("Unknown property '{0}'", PropertyEndDate));
			}

			var valueToMatch = propertyToMatch.GetValue(validationContext.ObjectInstance, null);
			if (valueToMatch == null || value == null)
			{
				return ValidationResult.Success;
			}

			return ((DateTime)value > (DateTime)valueToMatch) ? new ValidationResult(null) : ValidationResult.Success;
		}

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			yield return new ModelClientValidationRule
			             	{
			             		ErrorMessage = ErrorMessage,
			             		ValidationType = "comparewithenddate",
			             		ValidationParameters = {{"propertyenddate", string.Format("*.{0}", PropertyEndDate)}}
			             	};
		}
	}
}