using System;
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
	public class ShoppingCart
	{
		public string ShoppingCartId { get; set; }
		public ICollection<ShoppingCartItemDto> Items { get; set; }

		public void Add(ShoppingCartItemDto item)
		{
			Items.Add(item);
		}

		public void Add(int productId, int quantity, double price)
		{
			Items.Add(new ShoppingCartItemDto()
			{
				ProductId = productId,
				Quantity = quantity,
				Price = price
			});
		}

		public void UpdateProduct(int productId, int newQuantity, int newPrice)
		{
			var product = Items.SingleOrDefault(e => e.ProductId == productId);
			if (product != null)
			{
				product.Price = newPrice;
				product.Quantity = newQuantity;
			}
		}

		public void Remove(int productId)
		{
			var product = Items.SingleOrDefault(e => e.ProductId == productId);
			if (product != null)
				Items.Remove(product);
		}

		public void Clear()
		{
			Items.Clear();
		}

		public double GetTotalPrice()
		{
			return Items.Sum(product => product.TotalPrice);
		}

		public static ShoppingCart GetCart(IServiceProvider services)
		{
			if (services == null)
				throw new ArgumentNullException(nameof(services));

			try
			{
				ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
				string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
				session.SetString("CartId", cartId);

				return new ShoppingCart()
				{
					ShoppingCartId = cartId,
					Items = new List<ShoppingCartItemDto>()
				};
			}
			catch (InvalidOperationException ex)
			{
				Trace.WriteLine(ex.Message);
				throw new InvalidOperationException(ex.StackTrace);
			}
		}
	}
}
