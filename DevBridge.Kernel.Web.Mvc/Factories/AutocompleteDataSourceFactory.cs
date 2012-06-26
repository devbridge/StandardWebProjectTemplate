using System;
using DevBridge.Kernel.Web.Mvc.Contracts;
using StructureMap;

namespace DevBridge.Kernel.Web.Mvc.Factories
{
	public class AutocompleteDataSourceFactory
	{
		public static IAutocompleteDataSource GetDataSource(Type dataSourceType)
		{
			return (IAutocompleteDataSource) ObjectFactory.GetInstance(dataSourceType);
		} 
	}
}