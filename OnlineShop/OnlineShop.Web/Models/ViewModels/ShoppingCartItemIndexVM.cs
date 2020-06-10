using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Data.Models;

namespace OnlineShop.Web.Models.ViewModels
{
	public class ShoppingCartItemIndexVM
	{
		public int ProductId { get; set; }
		public string Title { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public double TotalPrice { get; set; }
		public string ImageUrl { get; set; }
	}
}
