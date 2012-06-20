using System.Collections.Generic;
using DevBridge.Kern.Models.Shared;

namespace DevBridge.Kern.Contracts
{
	public interface IAutocompleteDataSource
	{
		Autocomplete GetItem(int id);
		IEnumerable<Autocomplete> GetList(string term);
	}
}