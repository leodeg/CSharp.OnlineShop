using System;
using System.Collections.Generic;
using System.Linq;
using OnlineShop.Data.Extensions;
using OnlineShop.Data.Repositories;
using OnlineShop.Services.Dtos;
using OnlineShop.Services.ProductServices;

namespace OnlineShop.Services.Dtos
{
	public class ProductListWithFiltersDto
	{
		public ProductListWithFiltersDto(ProductFilterPageOptions options, IEnumerable<ProductListDto> products)
		{
			Options = options;
			Products = products;
		}

		public ProductFilterPageOptions Options { get; }
		public IEnumerable<ProductListDto> Products { get; }
	}
}
