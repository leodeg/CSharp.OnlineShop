using System;
using System.Collections.Generic;
using System.Linq;
using OnlineShop.Data.Extensions;
using OnlineShop.Data.Repositories;
using OnlineShop.Services.Dtos;
using OnlineShop.Services.ProductServices.QueryExtensions;

namespace OnlineShop.Services.ProductServices
{
	public class ListProductsService : IListProductsService
	{
		private readonly IProductRepository productRepository;

		public ListProductsService(IProductRepository productRepository)
		{
			this.productRepository = productRepository;
		}

		public IQueryable<ProductListDto> SortFilterPage(ProductFilterPageOptions options)
		{
			var products = productRepository
				.GetProductsForFilters()
				.MapToProductListDto()
				.ProductFilterBySubcategory(options.SubcategoryId)
				.ProductFilterByPrice(options.MinPrice, options.MaxPrice)
				.ProductFilterBySearchText(options.SearchText)
				.OrderProductsBy(options.OrderBy);

			options.UpdateOptionsProperties(products);
			return products.Page(options.CurrentPage - 1, options.PageSize);
		}
	}
}
