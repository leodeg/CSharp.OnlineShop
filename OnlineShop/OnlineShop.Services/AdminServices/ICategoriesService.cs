using System.Collections.Generic;
using OnlineShop.Data.Models;

namespace OnlineShop.Services.AdminServices
{
	public interface ICategoriesService
	{
		IEnumerable<Category> GetCategories();
		void RemoveCategory(int id);
		void SaveCategory(Category category);
		Category GetById(int id);
		IEnumerable<Category> GetWithSubcategories();
	}
}