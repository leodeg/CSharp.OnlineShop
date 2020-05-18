using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
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
			modelBuilder.Entity<Product>()
				.HasOne(e => e.Subcategory)
				.WithMany()
				.HasForeignKey(nameof(Product.SubcategoryId))
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Product>()
				.HasOne(e => e.Category)
				.WithMany()
				.HasForeignKey(nameof(Product.CategoryId))
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Product>()
				.Property(e => e.Created)
				.HasDefaultValueSql("getdate()");

			modelBuilder.Entity<Product>()
				.Property(e => e.Updated)
				.ValueGeneratedOnAddOrUpdate();
		}

		public DbSet<Promotion> Promotions { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Subcategory> Subcategories { get; set; }
		public DbSet<Product> Products { get; set; }
	}
}
