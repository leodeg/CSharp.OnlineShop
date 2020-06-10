﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Data.Repositories;

namespace OnlineShop.Services.OrderServices
{
	public class ShoppingCartItemDto
	{
		[Required]
		public int ProductId { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public double TotalPrice { get { return Price * Quantity; } }
	}
}
