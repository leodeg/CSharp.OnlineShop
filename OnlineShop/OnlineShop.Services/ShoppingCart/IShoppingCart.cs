using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Data.Repositories;

namespace OnlineShop.Services.ShoppingCart
{
	interface IShoppingCart
	{
		void Add(int productId, int quantity, double price);
		void Update(int productId, int newQuantity, int newPrice);
		void Remove(int productId);
		public void Clear();
		public double GetTotalPrice();
	}
}
