using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DevBridge.Templates.WebProject.AutocompleteDemo.Infrastructure.Extenssions
{
	public static class QueryableExtensions
	{
		public static IQueryable<T> WhereAny<T, TTerm>(this IQueryable<T> query, IEnumerable<TTerm> terms, Func<IQueryable<T>, TTerm, IQueryable<T>> func)
		{
			return terms.Aggregate(query, func);
		}

		public static IEnumerable<T> OrderBy<T, TKey>(this IEnumerable<T> query, ListSortDirection sortDirection, Func<T, TKey> predicate, IComparer<TKey> comparer)
		{
			return sortDirection == ListSortDirection.Ascending ? query.OrderBy(predicate, comparer) : query.OrderByDescending(predicate, comparer);
		}
	}
}