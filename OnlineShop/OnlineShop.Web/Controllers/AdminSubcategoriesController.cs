using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Models;
using OnlineShop.Services.AdminServices;
using OnlineShop.Web.Models.ViewModels;

namespace OnlineShop.Web.Controllers
{
	public class AdminSubcategoriesController : Controller
	{
		private readonly ISubcategoriesService subcategoriesService;
		private const string SubcategoryForm = "SubcategoryForm";

		public AdminSubcategoriesController(ISubcategoriesService subcategoriesService)
		{
			this.subcategoriesService = subcategoriesService;
		}

		public IActionResult Index(int? categoryId)
		{
			if (categoryId == null)
				return NotFound();

			var viewModel = new SubcategoriesIndexVM()
			{
				CategoryId = categoryId.Value,
				Subcategories = subcategoriesService.GetByCategoryId(categoryId.Value)
			};

			return View(viewModel);
		}

		public IActionResult Create(int? categoryId)
		{
			return View(SubcategoryForm, new Subcategory() { CategoryId = categoryId.Value });
		}

		public IActionResult Edit(int? id)
		{
			return View(SubcategoryForm, subcategoriesService.GetById(id.Value));
		}

		public IActionResult Save(Subcategory subcategory)
		{
			if (!ModelState.IsValid)
				return View(SubcategoryForm, subcategory);

			subcategoriesService.SaveSubcategory(subcategory);
			return RedirectToAction(nameof(Index), new { subcategory.CategoryId });
		}

		public IActionResult Delete(int? id)
		{
			int categoryId = subcategoriesService.GetById(id.Value).CategoryId;
			subcategoriesService.RemoveSubcategory(id.Value);
			return RedirectToAction(nameof(Index), new { categoryId });
		}
	}
}