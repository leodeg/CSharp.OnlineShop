using System.Collections.Generic;
using OnlineShop.Data.Models;
using OnlineShop.Services.Dtos;

namespace OnlineShop.Services.AdminServices
{
	public interface IProductsService
	{
		ProductInfoDto GetBaseInfoById(int id);
		IEnumerable<Product> GetProducts();
		void Remove(int id);
		IEnumerable<Product> GetWithSubcategory();
		void Save(ProductInfoDto product);
		Product GetById(int id);
	}
}