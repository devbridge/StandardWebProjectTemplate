using System;
using System.Web.Mvc;

namespace DevBridge.Kern.Infrastructure.ModelBinders
{
	public class TypeModelBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw new ArgumentNullException("bindingContext");
			}

			Type result = null;
			var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (value != null)
			{
				bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);
				try
				{
					result = Type.GetType(value.AttemptedValue);
				}
				catch (Exception ex)
				{
					bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex.Message);
				}
			}

			return result;
		}
	}
}