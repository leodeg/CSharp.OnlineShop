using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Data.Models;

namespace OnlineShop.Web.Models.ViewModels
{
	public class ShoppingCartIndexVM
	{
		public IEnumerable<ShoppingCartItemIndexVM> Items { get; set; }
		public double TotalPrice { get; set; }
	}
}
