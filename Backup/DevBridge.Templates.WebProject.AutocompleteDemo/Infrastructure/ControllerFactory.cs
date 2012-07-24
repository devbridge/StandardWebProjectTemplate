using System;
using System.Web.Mvc;
using StructureMap;

namespace DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure
{
	public class StructureMapControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
		{
			try
			{
				return ObjectFactory.GetInstance(controllerType) as Controller;
			}
			catch (StructureMapException e)
			{
				return base.GetControllerInstance(requestContext, controllerType);
			}
		}
	}
}