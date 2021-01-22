using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repositories;
using OnlineShop.Services.Dtos;

namespace OnlineShop.Services.OrderServices
{
	public class OrderService : IOrderService
	{
		private readonly IMapper mapper;
		private readonly IOrderRepository orderRepository;
		private readonly ICustomerRepository customerRepository;

		public OrderService(IMapper mapper, IOrderRepository orderRepository, ICustomerRepository customerRepository)
		{
			this.mapper = mapper;
			this.orderRepository = orderRepository;
			this.customerRepository = customerRepository;
		}

		public IEnumerable<Order> GetOrders()
		{
			return orderRepository.Get();
		}

		public void SaveOrder(Customer customer, IEnumerable<ShoppingCartItemDto> orderItems)
		{
			List<OrderItem> items = mapper.Map<List<OrderItem>>(orderItems);

			Customer currentCustomer = customerRepository.GetByAddress(customer.PostAddress);

			if (currentCustomer == default(Customer))
			{
				orderRepository.Create(new Order()
				{
					Customer = customer,
					OrderItems = items,
					TotalPrice = orderItems.Sum(products => products.TotalPrice),
					IsProcessed = false,
					ProcessedDate = DateTime.MinValue
				});
			}
			else
			{
				orderRepository.Create(new Order()
				{
					CustomerId = currentCustomer.Id,
					OrderItems = items,
					TotalPrice = orderItems.Sum(products => products.TotalPrice),
					IsProcessed = false,
					ProcessedDate = DateTime.MinValue
				});
			}

			orderRepository.SaveChanges();
		}
	}
}
