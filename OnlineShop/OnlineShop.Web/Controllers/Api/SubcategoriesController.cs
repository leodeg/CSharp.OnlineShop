using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.AdminServices;
using OnlineShop.Services.OrderServices;

namespace OnlineShop.Web.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubcategoriesController : ControllerBase
	{
		private readonly ISubcategoriesService subcategoriesService;

		public SubcategoriesController(ISubcategoriesService subcategoriesService)
		{
			this.subcategoriesService = subcategoriesService;
		}

		[HttpGet]
		public IActionResult Get(int? categoryId)
		{
			if (categoryId == null)
				return BadRequest();

			return new ObjectResult(subcategoriesService.GetByCategoryId(categoryId.Value));
		}
	}
}