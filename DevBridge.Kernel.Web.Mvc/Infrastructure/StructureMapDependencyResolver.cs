using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace DevBridge.Kernel.Web.Mvc.Infrastructure
{
	public class StructureMapDependencyResolver : IDependencyResolver
	{
		public object GetService(Type serviceType)
		{
			return ObjectFactory.TryGetInstance(serviceType, String.Empty);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return ObjectFactory.GetAllInstances(serviceType).Cast<object>();
		}
	}
}