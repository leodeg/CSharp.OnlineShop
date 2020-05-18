using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Data.Models;

namespace OnlineShop.Web.Models.ViewModels
{
	public class SubcategoriesIndexVM
	{
		public int CategoryId { get; set; }
		public IEnumerable<Subcategory> Subcategories { get; set; }
	}
}
