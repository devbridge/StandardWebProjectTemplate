namespace DevBridge.Templates.WebProject.AutocompleteDemo.Models.Shared
{
	public class Autocomplete
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public override string ToString()
		{
			return Id.ToString();
		}	}
}