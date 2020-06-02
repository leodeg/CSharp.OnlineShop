using System;
using System.Collections.Generic;
using System.Text;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repositories;

namespace OnlineShop.Services.AdminServices
{
	public class SubcategoriesService : ISubcategoriesService
	{
		private readonly ISubcategoryRepository subcategoryRepository;

		public SubcategoriesService(ISubcategoryRepository subcategoryRepository)
		{
			this.subcategoryRepository = subcategoryRepository;
		}

		public IEnumerable<Subcategory> GetSubcategories()
		{
			return subcategoryRepository.Get();
		}

		public IEnumerable<Subcategory> GetByCategoryId(int id)
		{
			return subcategoryRepository.GetByCategoryId(id);
		}

		public Subcategory GetById(int id)
		{
			return subcategoryRepository.Get(id);
		}

		public void SaveSubcategory(Subcategory subcategory)
		{
			if (subcategory.Id == 0)
				subcategoryRepository.Create(subcategory);
			else subcategoryRepository.Update(subcategory.Id, subcategory);
			subcategoryRepository.SaveChanges();
		}

		public void RemoveSubcategory(int id)
		{
			subcategoryRepository.Remove(id);
			subcategoryRepository.SaveChanges();
		}
	}
}
