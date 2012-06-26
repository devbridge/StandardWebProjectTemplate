using System.Collections.Generic;
using DevBridge.Kernel.Web.Mvc.Models.Shared;

namespace DevBridge.Kernel.Web.Mvc.Contracts
{
	public interface IAutocompleteDataSource
	{
		Autocomplete GetItem(int id);
		IEnumerable<Autocomplete> GetList(string term);
	}
}