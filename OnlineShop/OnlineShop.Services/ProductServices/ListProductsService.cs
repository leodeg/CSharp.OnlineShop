using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineShop.Data.Extensions;
using OnlineShop.Data.Repositories;
using OnlineShop.Services.Dtos;
using OnlineShop.Services.ProductServices.QueryObjects;

namespace OnlineShop.Services.ProductServices
{
	class ListProductsService
	{
		private readonly IProductRepository productRepository;

		public ListProductsService(IProductRepository productRepository)
		{
			this.productRepository = productRepository;
		}

		public IQueryable<ProductListDto> SortFilterPage (ProductFilterPageOptions options)
		{
			var products = productRepository
				.GetProductsForFilters()
				.MapToProductListDto()
				.ProductFilterBySubcategory(options.SubcategoryId)
				.ProductFilterByPrice(options.MinPrice, options.MaxPrice);

			return products.Page(options.PageNumber, options.PageSize);

		}
	}
}
