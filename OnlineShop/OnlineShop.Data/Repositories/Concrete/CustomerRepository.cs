using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OnlineShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Repositories
{
	public class CustomerRepository : Repository<Customer>, ICustomerRepository
	{
		public CustomerRepository(OnlineShopDbContext context) : base(context)
		{

		}

		private Customer GetCustomer (int id)
		{
			Customer customer = dbSet.FirstOrDefault(e => e.Id == id);
			if (customer == null)
				throw new InvalidOperationException($"Can't find customer with {id} id");
			return customer;
		}

		public override void Update(int id, Customer entity)
		{
			Customer customer = GetCustomer(id);
			customer.FullName = entity.FullName;
			customer.PhoneNumber = entity.PhoneNumber;
			customer.Email = entity.Email;
			customer.Country = entity.Country;
			customer.City = entity.City;
			customer.PostAddress = entity.PostAddress;
			customer.ZipCode = entity.ZipCode;
		}
	}
}
