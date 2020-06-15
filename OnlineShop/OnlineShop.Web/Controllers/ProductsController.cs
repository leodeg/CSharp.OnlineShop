using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repositories;
using OnlineShop.Models;
using OnlineShop.Services.AdminServices;
using OnlineShop.Services.Dtos;
using OnlineShop.Services.ProductServices;
using OnlineShop.Web.Models.ViewModels;

namespace OnlineShop.Web.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IListProductsService productsService;

		public ProductsController(IListProductsService productsService)
		{
			this.productsService = productsService;
		}

		public async Task<IActionResult> Index(ProductFilterPageOptions options)
		{
			if (options == null) return BadRequest();

			IEnumerable<ProductListDto> products = await productsService.SortFilterPage(options).ToListAsync();

			return View(new ProductIndexVM()
			{
				Options = new ProductListWithFiltersDto(options, products),
				PagingInformation = new PagingInformation()
				{
					CurrentPage = options.CurrentPage,
					ItemsPerPage = options.PageSize,
					TotalItems = options.TotalItemsCount
				}
			});
		}
	}
}