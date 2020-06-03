using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using OnlineShop.Data.Models;
using OnlineShop.Services.Dtos;

namespace OnlineShop.Services.ProductServices.QueryObjects
{
	public enum ProductFilterBy
	{
		NoFilter = 0,
		ByPublicationYear
	}

	public static class ProductListDtoFilter
	{
		public static IQueryable<ProductListDto> ProductFilterBy (this IQueryable<ProductListDto> products, ProductFilterBy filterBy)
		{
			switch (filterBy)
			{
				case QueryObjects.ProductFilterBy.NoFilter:
					return products;

				default:
					throw new ArgumentOutOfRangeException(nameof(filterBy), filterBy, null);
			}
		}

		public static IQueryable<ProductListDto> ProductFilterByCategory(this IQueryable<ProductListDto> products, int categoryId)
		{
			return products.Where(p => p.Category.Id == categoryId);
		}

		public static IQueryable<ProductListDto> ProductFilterBySubcategory(this IQueryable<ProductListDto> products, int subcategoryId)
		{
			return products.Where(p => p.Subcategory.Id == subcategoryId);
		}

		public static IQueryable<ProductListDto> ProductFilterByPrice (this IQueryable<ProductListDto> products, double min, double max)
		{
			return products.Where(p => p.ActualPrice > min && p.ActualPrice < max);
		}
	}
}
