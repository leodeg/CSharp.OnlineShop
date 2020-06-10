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
	public class OrderController : Controller
	{
		private readonly IOrderService orderService;
		private readonly ShoppingCart shoppingCart;

		public OrderController(IOrderService orderService, ShoppingCart shoppingCart)
		{
			this.orderService = orderService;
			this.shoppingCart = shoppingCart;
		}

		public IActionResult Checkout()
		{
			return View(new Customer());
		}

		[HttpPost]
		public IActionResult Checkout(Customer customer)
		{
			if (shoppingCart.Items.Count == 0)
				ModelState.AddModelError("", "Your cart is empty, add some pies first");

			if (ModelState.IsValid)
			{
				orderService.SaveOrder(customer, shoppingCart.Items);
				shoppingCart.Clear();
				return RedirectToAction(nameof(CheckoutComplete));
			}

			return View(customer);
		}

		public IActionResult CheckoutComplete()
		{
			ViewBag.CheckoutCompleteMessage = "Thanks for your order.";
			return View();
		}
	}
}