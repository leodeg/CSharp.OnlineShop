﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Data.Models;
using OnlineShop.Models;
using OnlineShop.Services.Dtos;
using OnlineShop.Services.ProductServices;

namespace OnlineShop.Web.Models.ViewModels
{

	public class ProductFormVM
	{
		public ProductInfoDto Product { get; set; }
		public IEnumerable<Category> Categories { get; set; }
	}
}
