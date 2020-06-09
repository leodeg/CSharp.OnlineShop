using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Data.Models;
using OnlineShop.Models;
using OnlineShop.Services.Dtos;
using OnlineShop.Services.ProductServices;

namespace OnlineShop.Web.Models.ViewModels
{

	public class ProductIndexVM
	{
		public ProductListWithFiltersDto Options { get; set; }
		public PagingInformation PagingInformation { get; set; }
	}
}
