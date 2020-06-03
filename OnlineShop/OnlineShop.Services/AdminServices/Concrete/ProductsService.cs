using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repositories;
using OnlineShop.Services.Dtos;
using OnlineShop.Services.FileServices;

namespace OnlineShop.Services.AdminServices
{
	public class ProductsService : IProductsService
	{
		private readonly IProductRepository productRepository;
		private readonly IMapper mapper;
		private readonly IImageService imageService;

		public ProductsService(IProductRepository productRepository, IMapper mapper, IImageService imageService)
		{
			this.productRepository = productRepository;
			this.mapper = mapper;
			this.imageService = imageService;
		}

		public Product GetById(int id)
		{
			return productRepository.Get(id);
		}


		public ProductInfoDto GetBaseInfoById(int id)
		{
			return mapper.Map<ProductInfoDto>(productRepository.Get(id));
		}

		public IEnumerable<Product> GetProducts()
		{
			return productRepository.Get();
		}

		public IEnumerable<Product> GetWithSubcategory()
		{
			return productRepository.GetWithSubcategory();
		}

		public void Save(ProductInfoDto productDto)
		{
			Product product = mapper.Map<Product>(productDto);

			if (product.Id == 0)
				productRepository.Create(product);
			else productRepository.Update(product.Id, product);
			productRepository.SaveChanges();
		}

		public void SaveWithImage(ProductInfoDto productDto, IFormFile image)
		{
			// TODO: refactoring
			Product product = mapper.Map<Product>(productDto);

			if (image != null)
			{
				if (!string.IsNullOrEmpty(product.ImageUrl))
					imageService.DeleteImage(product.ImageUrl);
				product.ImageUrl = imageService.SaveImage(image).Result;
			}

			if (product.Id == 0)
				productRepository.Create(product);
			else productRepository.Update(product.Id, product);

			productRepository.SaveChanges();
		}

		public PromotionDto GetPriceOffer(int productId)
		{
			return mapper.Map<PromotionDto>(productRepository.GetPriceOffer(productId));
		}

		public void SavePriceOffer(int productId, PromotionDto priceOffer)
		{
			productRepository.SavePriceOffer(productId, mapper.Map<Promotion>(priceOffer));
			productRepository.SaveChanges();
		}

		public void DeletePriceOffer(int productId)
		{
			productRepository.RemovePriceOffer(productId);
			productRepository.SaveChanges();
		}

		public string GetImageUrl(int productId)
		{
			return productRepository.GetImageUrl(productId);
		}

		public async Task ChangeImage(int productId, IFormFile image)
		{
			if (image != null)
			{
				DeleteOldImageFromLocalFiles(productId);
				string imageUrl = await imageService.SaveImage(image);
				productRepository.UpdateImageUrl(productId, imageUrl);
				productRepository.SaveChanges();
			}
		}

		private void DeleteOldImageFromLocalFiles(int productId)
		{
			string oldImageUrl = productRepository.GetImageUrl(productId);
			if (!string.IsNullOrEmpty(oldImageUrl))
				imageService.DeleteImage(oldImageUrl);
		}

		public void DeleteImage(int productId)
		{
			DeleteOldImageFromLocalFiles(productId);
			productRepository.DeleteImageUrl(productId);
			productRepository.SaveChanges();
		}

		public void SoftDelete(int productId)
		{
			productRepository.RemoveSoft(productId);
			productRepository.SaveChanges();
		}

		public void RestoreSoftDeleted(int productId)
		{
			productRepository.RestoreRemovedSoft(productId);
			productRepository.SaveChanges();
		}

		public void Remove(int id)
		{
			imageService.DeleteImage(productRepository.GetImageUrl(id));
			productRepository.Remove(id);
			productRepository.SaveChanges();
		}
	}
}
