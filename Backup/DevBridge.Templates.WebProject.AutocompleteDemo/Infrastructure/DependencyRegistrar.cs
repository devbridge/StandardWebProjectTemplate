using System;
using System.Reflection;
using StructureMap;

namespace DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure
{
	public static class DependencyRegistrar
	{
		public static void Register()
		{
			ObjectFactory.Initialize(x => x.Scan(scanner =>
			                                     	{
			                                     		scanner.TheCallingAssembly();
														scanner.AssembliesFromApplicationBaseDirectory(AsseblyFilter);
														scanner.WithDefaultConventions();
														scanner.LookForRegistries();
													}));
		}

		private static bool AsseblyFilter(Assembly assembly)
		{
			return assembly.FullName.StartsWith("AutocompleteTest.", StringComparison.InvariantCultureIgnoreCase);
		}
	}
}