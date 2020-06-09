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
	public static class ProductListDtoSelect
	{
		public static IQueryable<ProductListDto> MapToProductListDto (this IQueryable<Product> products)
		{
			return products.Select(p => new ProductListDto()
			{
				Id = p.Id,
				Title = p.Title,
				Price = p.Price,
				Quantity = p.Quantity,
				ShortDescription = p.ShortDescription,
				ImageUrl = p.ImageUrl,

				ActualPrice = p.Promotion == null ? p.Price : p.Promotion.PriceOffer,
				PromotionMessage = p.Promotion == null ? null : p.Promotion.Message,

				PublishedOn = p.Created,
				Category = p.Category,
				Subcategory = p.Subcategory
			});
		}
	}
}
