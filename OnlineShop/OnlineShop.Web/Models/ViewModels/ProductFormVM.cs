using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Data.Models;

namespace OnlineShop.Web.Models.ViewModels
{

	public class ProductFormVM
	{
		public Product Product { get; set; }
		public IEnumerable<Category> Categories { get; set; }
	}
}
