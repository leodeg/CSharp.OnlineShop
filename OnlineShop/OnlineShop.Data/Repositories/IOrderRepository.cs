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

	public interface IOrderRepository : IRepository<Order>
	{
		void UpdateCustomer(int id, int customerId);
		void AddOrderItem(int id, OrderItem item);
		void RemoveOrderItem(int id, OrderItem item);
		void SetProcessedDate(int id, DateTime processedDate);
		void RemoveProcessedDate(int id);
	}
}
