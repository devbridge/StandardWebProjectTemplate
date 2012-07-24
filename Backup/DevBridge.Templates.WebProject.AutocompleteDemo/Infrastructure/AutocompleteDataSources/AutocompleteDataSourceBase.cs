using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevBridge.Templates.WebProject.AutocompleteDemo.Models.Shared;

namespace DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure.AutocompleteDataSources
{
	public abstract class AutocompleteDataSourceBase<TEntity> : IAutocompleteDataSource
		where TEntity : IEntity, new()
	{
		protected IRepository Repository;
		protected ListSortDirection SortDirection { get; set; }
		protected IComparer<string> Comparer { get; set; }

		protected AutocompleteDataSourceBase(IRepository repository)
		{
			Repository = repository;
			SortDirection = ListSortDirection.Ascending;
		}

		public Autocomplete GetItem(int id)
		{
			var q = Repository.CreateQuery<TEntity>();
			return Select(q).FirstOrDefault();
		}

		public IEnumerable<Autocomplete> GetList(string term)
		{
			var q = Select(Filter(Repository.CreateQuery<TEntity>(), term)).ToList();
			return OrderBy(q).Take(20);
		}

		protected virtual List<Autocomplete> OrderBy(List<Autocomplete> autocompleteList)
		{
			return autocompleteList.OrderBy(SortDirection, x => x.Name, Comparer).ToList();
		}

		protected abstract IEnumerable<Autocomplete> Select(IQueryable<TEntity> query);
		protected abstract IQueryable<TEntity> Filter(IQueryable<TEntity> query, string term);

		protected static IEnumerable<string> SplitTerm(string term)
		{
			return string.IsNullOrWhiteSpace(term) ? new string[] { } : term.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}