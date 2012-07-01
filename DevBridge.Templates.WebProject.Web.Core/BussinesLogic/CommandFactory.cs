using System;
using StructureMap;

namespace DevBridge.Templates.WebProject.Common.Mvc
{
	public class CommandFactory
	{
		public static Func<Type, ICommand> GetDefaultService = type => (ICommand)ObjectFactory.GetInstance(type);
		public static TService GetService<TService>() where TService : ICommand
		{
			return (TService)GetDefaultService(typeof(TService));
		}
	}
}