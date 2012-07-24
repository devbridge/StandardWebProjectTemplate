using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure.Attributes;
using DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure.AutocompleteDataSources;
using DevBridge.Templates.WebProject.AutocompleteDemo.Models.Shared;

namespace DevBridge.Templates.WebProject.AutocompleteDemo.Models.Test
{
	public class Test
	{
		[Display(Name = "Food")]
		[Autocomplete(typeof(FoodAutocompleteDataSource))]
		public Autocomplete Food { get; set; }

		public Autocomplete LocalDataSource { get; set; }

		public object LocalDataSourceData
		{
			get
			{
				var serializer = new JavaScriptSerializer();
				var localDataSource = new 
				{
					suggestions = new[] { "Liberia", "Libyan Arab Jamahiriya", "Liechtenstein", "Lithuania" },
					data = new[] { "1", "2", "3", "4" }
				};
				return serializer.Serialize(localDataSource);
			}
		}
	}
}