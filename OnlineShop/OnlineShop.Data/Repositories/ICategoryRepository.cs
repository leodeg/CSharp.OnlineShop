﻿using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Repositories
{
	public interface ICategoryRepository : IRepository<Category>
	{
		IEnumerable<Category> GetWithSubcategories();
	}
}
