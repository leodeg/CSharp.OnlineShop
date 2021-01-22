using System;
using System.Collections.Generic;
using System.Linq;
using OnlineShop.Services.Dtos;

namespace OnlineShop.Services.ShoppingCart
{
	public class ShoppingCart : IShoppingCart
	{
		public string ShoppingCartId { get; set; }
		public ICollection<ShoppingCartItemDto> Items { get; set; }

		public void Add(ShoppingCartItemDto item)
		{
			Items.Add(item);
		}

		public void Add(int productId, int quantity, double price)
		{
			ShoppingCartItemDto product = Items.Where(p => p.ProductId == productId).FirstOrDefault();

			if (product == null)
			{
				Items.Add(new ShoppingCartItemDto()
				{
					ProductId = productId,
					Quantity = quantity,
					Price = price
				});
			}
			else
			{
				product.Quantity += quantity;
			}
		}

		public void Update(int productId, int newQuantity, int newPrice)
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
	}
}
