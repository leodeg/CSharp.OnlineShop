using System;
using System.Collections.Generic;
using System.Text;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repositories;

namespace OnlineShop.Services.AdminServices
{
	public class CategoriesService : ICategoriesService
	{
		private readonly ICategoryRepository categoryRepository;

		public CategoriesService(ICategoryRepository categoryRepository)
		{
			this.categoryRepository = categoryRepository;
		}

		public IEnumerable<Category> GetCategories()
		{
			return categoryRepository.Get();
		}

		public IEnumerable<Category> GetWithSubcategories()
		{
			return categoryRepository.GetWithSubcategories();
		}

		public Category GetById(int id)
		{
			return categoryRepository.Get(id);
		}

		public void SaveCategory(Category category)
		{
			if (category.Id == 0)
				categoryRepository.Create(category);
			else categoryRepository.Update(category.Id, category);
			categoryRepository.SaveChanges();
		}

		public void RemoveCategory(int id)
		{
			categoryRepository.Remove(id);
			categoryRepository.SaveChanges();
		}
	}
}
