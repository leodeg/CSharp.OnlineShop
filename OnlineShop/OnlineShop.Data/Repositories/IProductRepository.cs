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
	public interface IProductRepository : IRepository<Product>
	{
		IEnumerable<Product> GetWithPriceOffers();
		IEnumerable<Product> GetWithSubcategory();
		void UpdateQuantity(int productId, int quantity);
		void UpdateImageUrl(int productId, string imageUrl);
		void UpdatePriceOffer(Promotion priceOffer);
		void RemovePriceOffer(int productId);
		void UpdateCategoryAndSubcategory(int productId, int subcategoryId);
		void RemoveSoft(int productId);
		void RestoreRemovedSoft(int productId);
	}
}
