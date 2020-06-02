using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repositories;
using OnlineShop.Services.AdminServices;
using OnlineShop.Services.Dtos;
using OnlineShop.Web.Models.ViewModels;

namespace OnlineShop.Web.Controllers
{
	public class AdminProductsController : Controller
	{
		private readonly IProductsService productsService;
		private readonly ICategoriesService categoriesService;
		private const string ProductForm = "ProductForm";

		public AdminProductsController(IProductsService productsService, ICategoriesService categoriesService)
		{
			this.productsService = productsService;
			this.categoriesService = categoriesService;
		}

		public IActionResult Index()
		{
			return View(productsService.GetWithSubcategory());
		}

		public IActionResult Details(int? id)
		{
			return View(productsService.GetById(id.Value));
		}

		public IActionResult Create()
		{
			return View(ProductForm, new ProductFormVM()
			{
				Product = new ProductInfoDto(),
				Categories = categoriesService.GetCategories()
			});
		}

		public IActionResult Edit(int? id)
		{
			return View(ProductForm, new ProductFormVM()
			{
				Product = productsService.GetBaseInfoById(id.Value),
				Categories = categoriesService.GetCategories()
			});
		}

		public IActionResult Save(ProductInfoDto product, IFormFile image)
		{
			if (!ModelState.IsValid)
				return View(ProductForm, product);

			if (image != null)
				productsService.SaveWithImage(product, image);
			else productsService.Save(product);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int? id)
		{
			productsService.Remove(id.Value);
			return RedirectToAction(nameof(Index));
		}
	}
}