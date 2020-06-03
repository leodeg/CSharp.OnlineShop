using System.Collections.Generic;
using OnlineShop.Data.Models;
using OnlineShop.Services.Dtos;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

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
		void SaveWithImage(ProductInfoDto productDto, IFormFile image);
		void SavePriceOffer(int productId, PromotionDto priceOffer);
		void DeletePriceOffer(int productId);
		Task ChangeImage(int productId, IFormFile image);
		void SoftDelete(int productId);
		void RestoreSoftDeleted(int productId);
		PromotionDto GetPriceOffer(int productId);
		string GetImageUrl(int productId);
		void DeleteImage(int productId);
	}
}