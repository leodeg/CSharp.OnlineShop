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
using OnlineShop.Services.AdminServices;
using OnlineShop.Services.Dtos;
using OnlineShop.Services.ProductServices;
using OnlineShop.Web.Models.ViewModels;

namespace OnlineShop.Web.Controllers
{
	public class AdminProductsController : Controller
	{
		private readonly IProductsService productsService;
		private readonly ICategoriesService categoriesService;
		private const string ProductForm = "ProductForm";
		private const string PriceOfferForm = "PriceOfferForm";
		private const string ChangeImageForm = "ChangeImageForm";

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

		#region Promotion

		public IActionResult CreatePromotion(int productId)
		{
			return View(PriceOfferForm, new PromotionDto() { ProductId = productId });
		}

		public IActionResult EditPromotion(int productId)
		{
			PromotionDto promotion = productsService.GetPriceOffer(productId);

			if (promotion == null)
				return BadRequest();

			return View(PriceOfferForm, promotion);
		}

		public IActionResult SavePromotion(int? productId, PromotionDto promotion)
		{
			if (productId == null)
				return BadRequest();

			if (!ModelState.IsValid)
			{
				return View(PriceOfferForm, promotion);
			}

			productsService.SavePriceOffer(productId.Value, promotion);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult DeletePromotion(int? productId)
		{
			productsService.DeletePriceOffer(productId.Value);
			return RedirectToAction(nameof(Index));
		}

		#endregion

		public IActionResult ChangeImage(int? productId)
		{
			return View(ChangeImageForm, new ProductChangeImageVM()
			{
				ProductId = productId.Value,
				ImageUrl = productsService.GetImageUrl(productId.Value)
			});
		}

		public async Task<IActionResult> SaveImage(int? productId, IFormFile image)
		{
			if (productId == null)
				return BadRequest();

			await productsService.ChangeImage(productId.Value, image);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult DeleteImage (int? productId)
		{
			if (productId == null)
				return BadRequest();

			productsService.DeleteImage(productId.Value);
			return RedirectToAction(nameof(Index));
		}
	}
}