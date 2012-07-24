using System.Collections.Generic;
using DevBridge.Templates.WebProject.AutocompleteDemo.Models.Shared;

namespace DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure.AutocompleteDataSources
{
	public interface IAutocompleteDataSource
	{
		Autocomplete GetItem(int id);
		IEnumerable<Autocomplete> GetList(string term);
	}
}