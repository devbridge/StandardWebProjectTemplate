using System;
using DevBridge.Kern.Contracts;
using StructureMap;

namespace DevBridge.Kern.Factories
{
	public class AutocompleteDataSourceFactory
	{
		public static IAutocompleteDataSource GetDataSource(Type dataSourceType)
		{
			return (IAutocompleteDataSource) ObjectFactory.GetInstance(dataSourceType);
		} 
	}
}