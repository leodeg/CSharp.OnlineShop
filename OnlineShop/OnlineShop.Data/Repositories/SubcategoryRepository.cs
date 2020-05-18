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
	public class SubcategoryRepository : Repository<Subcategory>, ISubcategoryRepository
	{
		public SubcategoryRepository(OnlineShopDbContext context) : base(context)
		{
		}

		public override void Update(int id, Subcategory entity)
		{
			if (entity == null)
				throw new ArgumentNullException();

			Subcategory subcategory = dbSet.FirstOrDefault(e => e.Id == id);
			if (subcategory == null)
				throw new InvalidOperationException($"Can't find subcategory with {id} id");

			subcategory.Title = entity.Title;

			if (subcategory.CategoryId != entity.CategoryId)
				subcategory.CategoryId = entity.CategoryId;
		}
	}
}
