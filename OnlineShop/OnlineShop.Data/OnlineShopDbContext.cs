using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Data
{
	public class OnlineShopDbContext : DbContext
	{
		public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options)
			: base(options)
		{
		}
	}
}
