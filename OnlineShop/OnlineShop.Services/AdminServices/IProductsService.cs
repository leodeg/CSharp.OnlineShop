using System.Collections.Generic;
using OnlineShop.Data.Models;

namespace OnlineShop.Services.AdminServices
{
	public interface IProductsService
	{
		Product GetById(int id);
		IEnumerable<Product> GetProducts();
		void Remove(int id);
		void Save(Product product);
		IEnumerable<Product> GetWithSubcategory();
	}
}