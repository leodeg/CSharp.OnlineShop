using System;
using System.Collections.Generic;
using System.Text;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repositories;

namespace OnlineShop.Services.AdminServices
{
	public class ProductsService : IProductsService
	{
		private readonly IProductRepository productRepository;

		public ProductsService(IProductRepository productRepository)
		{
			this.productRepository = productRepository;
		}

		public Product GetById(int id)
		{
			return productRepository.Get(id);
		}

		public IEnumerable<Product> GetProducts()
		{
			return productRepository.Get();
		}

		public void Save(Product product)
		{
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
