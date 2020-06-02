using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repositories;
using OnlineShop.Services.Dtos;

namespace OnlineShop.Services.AdminServices
{
	public class ProductsService : IProductsService
	{
		private readonly IProductRepository productRepository;
		private readonly IMapper mapper;

		public ProductsService(IProductRepository productRepository, IMapper mapper)
		{
			this.productRepository = productRepository;
			this.mapper = mapper;
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

		public void Remove(int id)
		{
			productRepository.Remove(id);
			productRepository.SaveChanges();
		}
	}
}
