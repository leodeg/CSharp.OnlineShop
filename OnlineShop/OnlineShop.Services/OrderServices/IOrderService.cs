using System.Collections.Generic;
using OnlineShop.Data.Models;

namespace OnlineShop.Services.OrderServices
{
	public interface IOrderService
	{
		IEnumerable<Order> GetOrders();
		void SaveOrder(Customer customer, IEnumerable<ShoppingCartItemDto> orderItems);
	}
}