using System.Collections.Generic;
using OnlineShop.Data.Models;

namespace OnlineShop.Services.AdminServices
{
	public interface ISubcategoriesService
	{
		IEnumerable<Subcategory> GetSubcategories();
		void RemoveSubcategory(int id);
		void SaveSubcategory(Subcategory subcategory);
		IEnumerable<Subcategory> GetByCategoryId(int id);
		Subcategory GetById(int id);
	}
}