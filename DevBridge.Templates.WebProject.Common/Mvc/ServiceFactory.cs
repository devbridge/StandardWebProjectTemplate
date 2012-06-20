using System;
using StructureMap;

namespace DevBridge.Templates.WebProject.Common.Mvc
{
	public class ServiceFactory
	{
		public static Func<Type, IService> GetDefaultService = type => (IService)ObjectFactory.GetInstance(type);
		public static TService GetService<TService>() where TService : IService
		{
			return (TService)GetDefaultService(typeof(TService));
		}
	}
}