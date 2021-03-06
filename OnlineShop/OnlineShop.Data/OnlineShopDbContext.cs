﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Configurations;
using OnlineShop.Data.Models;

namespace OnlineShop.Data
{
	public class OnlineShopDbContext : DbContext
	{
		public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ProductConfig());
			modelBuilder.ApplyConfiguration(new PromotionConfig());
			modelBuilder.ApplyConfiguration(new CategoryConfig());
			modelBuilder.ApplyConfiguration(new OrderConfig());
			modelBuilder.ApplyConfiguration(new OrderItemConfig());
		}

		public DbSet<Promotion> Promotions { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Subcategory> Subcategories { get; set; }
		public DbSet<Product> Products { get; set; }

		public DbSet<Order> Orders { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
	}
}
