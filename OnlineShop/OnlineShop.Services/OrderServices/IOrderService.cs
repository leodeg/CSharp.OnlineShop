using System.Collections.Generic;
using OnlineShop.Data.Models;
using OnlineShop.Services.Dtos;

namespace OnlineShop.Services.OrderServices
{
	public interface IOrderService
	{
		IEnumerable<Order> GetOrders();
		void SaveOrder(Customer customer, IEnumerable<ShoppingCartItemDto> orderItems);
	}
}