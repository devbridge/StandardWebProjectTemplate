using System.Web.Mvc;
using DevBridge.Kern.Models.Shared;

namespace DevBridge.Kern.Infrastructure.ModelBinders
{
	public class AutocompleteModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if(!bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
			{
				return null;
			}

			var nameResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".Name");
			if(nameResult == null || string.IsNullOrWhiteSpace(nameResult.AttemptedValue))
			{
				return null;
			}

			var idResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (idResult == null || string.IsNullOrWhiteSpace(idResult.AttemptedValue))
			{
				idResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".Id");
				if (idResult == null || string.IsNullOrWhiteSpace(idResult.AttemptedValue))
				{
					return null;
				}
			}

			//return new Autocomplete { Id = int.Parse(((string[])idResult.RawValue)[0]), Name = ((string[])nameResult.RawValue)[0] };
			return new Autocomplete { Id = int.Parse(idResult.AttemptedValue), Name = nameResult.AttemptedValue };
		}
	}
}