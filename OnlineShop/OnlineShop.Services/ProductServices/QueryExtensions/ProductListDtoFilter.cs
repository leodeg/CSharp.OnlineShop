using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using OnlineShop.Data.Models;
using OnlineShop.Services.Dtos;

namespace OnlineShop.Services.ProductServices.QueryExtensions
{
	public enum ProductFilterBy
	{
		NoFilter = 0,
	}

	public static class ProductListDtoFilter
	{
		public static IQueryable<ProductListDto> ProductFilterBy (this IQueryable<ProductListDto> products, ProductFilterBy filterBy)
		{
			switch (filterBy)
			{
				case QueryExtensions.ProductFilterBy.NoFilter:
					return products;

				default:
					throw new ArgumentOutOfRangeException(nameof(filterBy), filterBy, null);
			}
		}

		public static IQueryable<ProductListDto> ProductFilterByCategory(this IQueryable<ProductListDto> products, int categoryId = 0)
		{
			if (categoryId == 0)
				return products;

			return products.Where(p => p.Category.Id == categoryId);
		}

		public static IQueryable<ProductListDto> ProductFilterBySubcategory(this IQueryable<ProductListDto> products, int subcategoryId = 0)
		{
			if (subcategoryId == 0)
				return products;

			return products.Where(p => p.Subcategory.Id == subcategoryId);
		}

		public static IQueryable<ProductListDto> ProductFilterByPrice (this IQueryable<ProductListDto> products, double min = 0, double max = 0)
		{
			if (min == 0 && max == 0)
				return products;

			return products.Where(p => p.ActualPrice >= min && p.ActualPrice <= max);
		}

		public static IQueryable<ProductListDto> ProductFilterBySearchText(this IQueryable<ProductListDto> products, string searchText)
		{
			if (string.IsNullOrEmpty(searchText))
				return products;

			return products.Where(p => p.Title.Contains(searchText) || p.ShortDescription.Contains(searchText));
		}
	}
}
