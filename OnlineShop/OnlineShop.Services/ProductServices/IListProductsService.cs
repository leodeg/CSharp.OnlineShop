using System.Linq;
using OnlineShop.Services.Dtos;

namespace OnlineShop.Services.ProductServices
{
	public interface IListProductsService
	{
		IQueryable<ProductListDto> SortFilterPage(ProductFilterPageOptions options);
	}
}