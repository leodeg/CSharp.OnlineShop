using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Data.Extensions
{
	public static class GenericPagingExtension
	{
		public static IQueryable<T> Page<T>(this IQueryable<T> query, int currentPage, int elementsOnPage)
		{
			if (query == null || query.Count() < 1)
				return query;

			if (elementsOnPage == 0)
				throw new ArgumentOutOfRangeException (nameof(elementsOnPage), "pageSize cannot be zero.");

			if (currentPage != 0)
				query = query.Skip(currentPage * elementsOnPage);
			return query.Take(elementsOnPage);
		}
	}
}
