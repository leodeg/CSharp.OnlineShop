using System;
using System.Collections.Generic;
using System.Text;
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
			Product product = mapper.Map<Product>(productDto);

			if (product.Id == 0)
			{
				if (image != null)
					product.ImageUrl = imageService.SaveImage(image).Result;
				productRepository.Create(product);
			}
			else
			{
				if (image != null)
				{
					if (!string.IsNullOrEmpty(product.ImageUrl))
						imageService.DeleteImage(product.ImageUrl);
					product.ImageUrl = imageService.SaveImage(image).Result;
				}
				productRepository.Update(product.Id, product);
			}

			productRepository.SaveChanges();
		}

		// TODO: add change image feature
		public void ChangeImage(int productId, IFormFile image)
		{
			throw new NotImplementedException();
		}

		// TODO: add soft delete feature
		public void SoftDelete(int id)
		{
			throw new NotImplementedException();
		}

		public void Remove(int id)
		{
			imageService.DeleteImage(productRepository.GetImageUrl(id));
			productRepository.Remove(id);
			productRepository.SaveChanges();
		}
	}
}
