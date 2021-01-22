using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repositories;
using OnlineShop.Models;
using OnlineShop.Services.AdminServices;
using OnlineShop.Services.Dtos;
using OnlineShop.Services.OrderServices;
using OnlineShop.Services.ProductServices;
using OnlineShop.Services.ShoppingCart;
using OnlineShop.Web.Extensions;
using OnlineShop.Web.Models.ViewModels;

namespace OnlineShop.Web.Controllers
{
	public class ShoppingCartController : Controller
	{
		private readonly IProductsService productsService;
		private readonly IServiceProvider services;

		public ShoppingCartController(IProductsService productsService, IServiceProvider services)
		{
			this.productsService = productsService;
			this.services = services;
		}

		public IActionResult Index()
		{
			ShoppingCart shoppingCart = GetCart(services);

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
				TotalPrice = items.Sum(e => e.TotalPrice),
				Count = items.Count()
			};

			return View(vm);
		}

		[HttpPost]
		public IActionResult AddItem(int productId, int quantity, double price, string redirectUrl)
		{
			ShoppingCart shoppingCart = GetCart(services);
			shoppingCart.Add(productId, quantity, price);
			SaveCart(services, shoppingCart);

			return Redirect(redirectUrl);
		}

		public IActionResult RemoveItem(int? id)
		{
			if (id == null)
				return BadRequest();

			ShoppingCart shoppingCart = GetCart(services);
			shoppingCart.Remove(id.Value);
			SaveCart(services, shoppingCart);

			return RedirectToAction(nameof(Index));
		}

		public static void SaveCart(IServiceProvider services, ShoppingCart cart)
		{
			ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
			session.SetComplexData(cart.ShoppingCartId, cart);
		}

		public static ShoppingCart GetCart(IServiceProvider services)
		{
			if (services == null)
				throw new ArgumentNullException(nameof(services));

			const string CART_ID = "CardId";

			ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
			ShoppingCart shoppingCart = session.GetComplexData<ShoppingCart>(CART_ID);

			if (shoppingCart == null || shoppingCart == default(ShoppingCart))
			{
				return new ShoppingCart()
				{
					ShoppingCartId = CART_ID,
					Items = new List<ShoppingCartItemDto>()
				};
			}

			return shoppingCart;
		}
	}
}