using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.AdminServices;

namespace OnlineShop.Web.Components
{
	public class CategoriesMenu : ViewComponent
	{
		private readonly ICategoriesService categoriesService;

		public CategoriesMenu(ICategoriesService categoriesService)
		{
			this.categoriesService = categoriesService;
		}

		public IViewComponentResult Invoke()
		{
			var categories = categoriesService.GetWithSubcategories();
			return View(categories);
		}
	}
}
