using System;
using StructureMap;

namespace DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure.AutocompleteDataSources
{
	public class AutocompleteDataSourceFactory
	{
		public static IAutocompleteDataSource GetDataSource(Type dataSourceType)
		{
			return (IAutocompleteDataSource) ObjectFactory.GetInstance(dataSourceType);
		} 
	}
}