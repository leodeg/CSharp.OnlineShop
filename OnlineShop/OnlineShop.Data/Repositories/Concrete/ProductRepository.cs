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
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		public ProductRepository(OnlineShopDbContext context) : base(context)
		{
		}

		public IEnumerable<Product> GetWithPriceOffers()
		{
			return dbSet.AsNoTracking().Include(e => e.Promotion).ToList();
		}

		public IEnumerable<Product> GetWithSubcategory()
		{
			return dbSet.AsNoTracking().Include(e => e.Subcategory).ToList();
		}

		public string GetImageUrl(int id)
		{
			return dbSet.SingleOrDefault(e => e.Id == id)?.ImageUrl;
		}

		public override void Update(int id, Product entity)
		{
			if (entity == null)
				throw new ArgumentNullException();

			Product product = dbSet.FirstOrDefault(e => e.Id == id);
			if (product == null)
				throw new InvalidOperationException($"Can't find product with {id} id");

			product.Title = entity.Title;
			product.Price = entity.Price;
			product.Description = entity.Description;
			product.ShortDescription = entity.ShortDescription;
			product.Quantity = entity.Quantity;
			product.ImageUrl = entity.ImageUrl;

			if (entity.CategoryId != product.CategoryId || entity.SubcategoryId != product.SubcategoryId)
				UpdateCategoryAndSubcategory(id, entity.SubcategoryId);
		}

		public void UpdateQuantity(int productId, int quantity)
		{
			if (quantity < 0)
				throw new ArgumentOutOfRangeException("Quantity must be positive value!");

			Product product = dbSet.FirstOrDefault(e => e.Id == productId);
			product.Quantity = quantity;
		}

		public void UpdateImageUrl(int productId, string imageUrl)
		{
			if (imageUrl != null)
			{
				Product product = dbSet.FirstOrDefault(e => e.Id == productId);

				if (!product.ImageUrl.Equals(imageUrl))
					product.ImageUrl = imageUrl;
			}
		}

		public void UpdatePriceOffer(Promotion priceOffer)
		{
			Product product = dbSet.Include(e => e.Promotion).FirstOrDefault(e => e.Id == priceOffer.ProductId);

			if (product.Promotion == null)
			{
				product.Promotion = priceOffer;
			}
			else
			{
				product.Promotion.PriceOffer = priceOffer.PriceOffer;
				product.Promotion.Message = priceOffer.Message;
			}
		}

		public void RemovePriceOffer(int productId)
		{
			Product product = dbSet.Include(e => e.Promotion).FirstOrDefault(e => e.Id == productId);
			product.Promotion = null;
		}

		public void UpdateCategoryAndSubcategory(int productId, int subcategoryId)
		{
			Product product = dbSet.Include(e => e.Category).Include(e => e.Subcategory).FirstOrDefault(e => e.Id == productId);
			Subcategory subcategory = context.Subcategories.Include(e => e.Category).FirstOrDefault(e => e.Id == subcategoryId);

			product.CategoryId = subcategory.CategoryId;
			product.SubcategoryId = subcategoryId;
		}

		public void RemoveSoft(int productId)
		{
			Product product = dbSet.FirstOrDefault(e => e.Id == productId);
			product.SoftDeleted = true;
		}

		public void RestoreRemovedSoft(int productId)
		{
			Product product = dbSet.FirstOrDefault(e => e.Id == productId);
			product.SoftDeleted = false;
		}
	}
}
