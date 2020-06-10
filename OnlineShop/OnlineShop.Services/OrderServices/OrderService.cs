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

namespace OnlineShop.Services.OrderServices
{
	public class OrderService : IOrderService
	{
		private readonly IMapper mapper;
		private readonly IOrderRepository orderRepository;

		public OrderService(IMapper mapper, IOrderRepository orderRepository)
		{
			this.mapper = mapper;
			this.orderRepository = orderRepository;
		}

		public IEnumerable<Order> GetOrders()
		{
			return orderRepository.Get();
		}

		public void SaveOrder(Customer customer, IEnumerable<ShoppingCartItemDto> orderItems)
		{
			List<OrderItem> items = mapper.Map<List<OrderItem>>(orderItems);

			Order order = new Order()
			{
				Customer = customer,
				OrderItems = items,
				TotalPrice = orderItems.Sum(products => products.TotalPrice),
			};

			orderRepository.Create(order);
			orderRepository.SaveChanges();
		}
	}
}
