using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using OnlineShop.Services.Dtos;

namespace OnlineShop.Services.ProductServices.QueryExtensions
{
	public enum ProductOrderBy
	{
		Default = 0,
		ByPublicationYear,
		ByPriceLowestFirst,
		ByPriceHigestFirst
	}

	public static class ProductListDtoSort
	{
		public static IQueryable<ProductListDto> OrderProductsBy(this IQueryable<ProductListDto> products, ProductOrderBy orderBy)
		{
			if (products == null || products.Count() < 1)
				return products;

			switch (orderBy)
			{
				case ProductOrderBy.Default:
					return products.OrderByDescending(product => product.Id);

				case ProductOrderBy.ByPublicationYear:
					return products.OrderByDescending(product => product.PublishedOn);

				case ProductOrderBy.ByPriceLowestFirst:
					return products.OrderBy(product => product.ActualPrice);

				case ProductOrderBy.ByPriceHigestFirst:
					return products.OrderByDescending(product => product.ActualPrice);

				default:
					throw new ArgumentOutOfRangeException(nameof(orderBy), orderBy, null);
			}
		}
	}
}
