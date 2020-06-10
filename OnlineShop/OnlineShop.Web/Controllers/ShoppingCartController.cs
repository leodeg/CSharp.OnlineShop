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
using OnlineShop.Services.OrderServices;
using OnlineShop.Services.ProductServices;
using OnlineShop.Web.Models.ViewModels;

namespace OnlineShop.Web.Controllers
{
	public class ShoppingCartController : Controller
	{
		private readonly IProductsService productsService;
		private readonly ShoppingCart shoppingCart;

		public ShoppingCartController(IProductsService productsService, ShoppingCart shoppingCart)
		{
			this.productsService = productsService;
			this.shoppingCart = shoppingCart;
		}

		public IActionResult Index()
		{
			List<ShoppingCartItemIndexVM> items = new List<ShoppingCartItemIndexVM>();
			foreach (var item in shoppingCart.Items)
			{
				var product = productsService.GetById(item.ProductId);
				items.Add(new ShoppingCartItemIndexVM()
				{
					ProductId = item.ProductId,
					Title = product.Title,
					Price = item.Price,
					Quantity = item.Quantity,
					TotalPrice = item.TotalPrice,
					ImageUrl = product.ImageUrl
				});
			}

			ShoppingCartIndexVM vm = new ShoppingCartIndexVM()
			{
				Items = items,
				TotalPrice = items.Sum(e => e.TotalPrice)
			};

			return View(vm);
		}

		public IActionResult RemoveItem(int? id)
		{
			if (id == null)
				return BadRequest();
			shoppingCart.Remove(id.Value);
			return RedirectToAction(nameof(Index));
		}
	}
}