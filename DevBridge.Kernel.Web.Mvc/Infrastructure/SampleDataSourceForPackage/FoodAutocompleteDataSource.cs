/*using System.Collections.Generic;
using System.Linq;
using DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure.Tools;
using DevBridge.Templates.WebProject.AutocompleteDemo.Models.Shared;

namespace DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure.AutocompleteDataSources
{
	public class FoodAutocompleteDataSource : AutocompleteDataSourceBase<FoodEntity>
	{
		public FoodAutocompleteDataSource(IRepository repository) : base(repository)
		{
			Comparer = new NaturalComparer();
		}

		protected override IEnumerable<Autocomplete> Select(IQueryable<FoodEntity> query)
		{
			return query.Select(e => new Autocomplete
			{
				Id = e.Id,
				Name = e.Name
			});
		}

		protected override IQueryable<FoodEntity> Filter(IQueryable<FoodEntity> query, string term)
		{
			return query.WhereAny(SplitTerm(term), (q, t) => q.Where(e => e.Name.Contains(t)));
		}
	}
}*/