using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Models;
using OnlineShop.Services.AdminServices;

namespace OnlineShop.Web.Controllers
{
	public class AdminCategoriesController : Controller
	{
		private readonly ICategoriesService categoriesService;
		private const string CategoryForm = "CategoryForm";

		public AdminCategoriesController(ICategoriesService categoriesService)
		{
			this.categoriesService = categoriesService;
		}

		public IActionResult Index()
		{
			return View(categoriesService.GetCategories());
		}

		public IActionResult Create()
		{
			return View(CategoryForm, new Category());
		}

		public IActionResult Edit(int? id)
		{
			return View(CategoryForm, categoriesService.GetById(id.Value));
		}

		public IActionResult Save(Category category)
		{
			if (!ModelState.IsValid)
				return View(CategoryForm, category);

			categoriesService.SaveCategory(category);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete (int? id)
		{
			categoriesService.RemoveCategory(id.Value);
			return RedirectToAction(nameof(Index));
		}
	}
}