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
using OnlineShop.Services.ShoppingCart;
using OnlineShop.Web.Extensions;
using OnlineShop.Web.Models.ViewModels;

namespace OnlineShop.Web.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderService orderService;
		private readonly IServiceProvider service;

		public OrderController(IOrderService orderService, IServiceProvider service)
		{
			this.orderService = orderService;
			this.service = service;
		}

		public IActionResult Checkout()
		{
			return View(new Customer());
		}

		[HttpPost]
		public IActionResult Checkout(Customer customer)
		{
			ShoppingCart shoppingCart = ShoppingCartController.GetCart(service);

			if (shoppingCart.Items.Count == 0)
				ModelState.AddModelError("", "Your cart is empty, add some pies first");

			if (ModelState.IsValid)
			{
				orderService.SaveOrder(customer, shoppingCart.Items);
				shoppingCart.Clear();
				ShoppingCartController.SaveCart(service, shoppingCart);

				return RedirectToAction(nameof(CheckoutComplete));
			}

			return View(customer);
		}

		public IActionResult CheckoutComplete()
		{
			return View(nameof(CheckoutComplete), "Your order was complete.");
		}
	}
}