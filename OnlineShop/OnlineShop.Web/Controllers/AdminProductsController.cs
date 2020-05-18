using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repositories;
using OnlineShop.Services.AdminServices;

namespace OnlineShop.Web.Controllers
{
	public class AdminProductsController : Controller
	{
		private readonly IProductsService productsService;
		private const string ProductForm = "ProductForm";


		public AdminProductsController(IProductsService productsService)
		{
			this.productsService = productsService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Create()
		{
			return View(ProductForm, new Product());
		}

		public IActionResult Edit(int? id)
		{
			return View(ProductForm, productsService.GetById(id.Value));
		}

		public IActionResult Save(Product product)
		{
			if (!ModelState.IsValid)
				return View(ProductForm, product);

			productsService.Save(product);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int? id)
		{
			productsService.Remove(id.Value);
			return RedirectToAction(nameof(Index));
		}
	}
}