using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Repositories
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		public CategoryRepository(OnlineShopDbContext context) : base(context)
		{
		}

		public IEnumerable<Category> GetWithSubcategories()
		{
			return dbSet.AsNoTracking().Include(e => e.Subcategories);
		}

		private Category GetCategory(int id)
		{
			var category = dbSet.SingleOrDefault(e => e.Id == id);
			if (category == null)
				throw new InvalidOperationException($"Can't find category with {id} id");
			return category;
		}

		public void AddSubcategory (int id, Subcategory subcategory)
		{
			Category category = GetCategory(id);
			category.Subcategories.Add(subcategory);
		}

		public override void Update(int id, Category entity)
		{
			if (entity == null)
				throw new ArgumentNullException();
			Category category = GetCategory(id);
			category.Title = entity.Title;
		}

		public override void Remove(Category entity)
		{
			dbSet.Remove(entity);
		}

		public override void Remove(int id)
		{
			Category category = dbSet.Include(e => e.Subcategories).FirstOrDefault(e => e.Id == id);
			if (category == null)
				throw new InvalidOperationException($"Can't find category with {id} id");
			dbSet.Remove(category);
		}
	}
}
