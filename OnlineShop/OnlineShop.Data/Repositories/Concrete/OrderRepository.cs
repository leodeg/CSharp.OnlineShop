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
	public class OrderRepository : Repository<Order>, IOrderRepository
	{
		public OrderRepository(OnlineShopDbContext context) : base(context)
		{
		}

		private Order GetOrder(int id)
		{
			Order order = dbSet.FirstOrDefault(e => e.Id == id);
			if (order == null)
				throw new InvalidOperationException($"Can't find order with {id} id");
			return order;
		}

		public override void Update(int id, Order entity)
		{
			if (entity == null)
				throw new ArgumentNullException();

			Order order = GetOrder(id);

			order.TotalPrice = entity.TotalPrice;
			order.OrderDate = entity.OrderDate;
			order.ProcessedDate = entity.ProcessedDate;
			order.IsProcessed = entity.IsProcessed;
			order.CustomerId = entity.CustomerId;
		}

		public void UpdateCustomer(int id, int customerId)
		{
			Order order = GetOrder(id);
			order.CustomerId = customerId;
		}

		public void AddOrderItem(int id, OrderItem item)
		{
			Order order = GetOrder(id);
			order.OrderItems.Add(item);
		}

		public void RemoveOrderItem(int id, OrderItem item)
		{
			Order order = GetOrder(id);
			order.OrderItems.Remove(item);
		}

		public void SetProcessedDate(int id, DateTime processedDate)
		{
			Order order = GetOrder(id);
			order.ProcessedDate = processedDate;
			order.IsProcessed = true;
		}

		public void RemoveProcessedDate(int id)
		{
			Order order = GetOrder(id);
			order.ProcessedDate = null;
			order.IsProcessed = false;
		}
	}
}
