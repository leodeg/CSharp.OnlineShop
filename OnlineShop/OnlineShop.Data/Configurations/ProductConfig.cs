using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Configurations
{
	class ProductConfig : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder
				.HasOne(e => e.Subcategory)
				.WithMany()
				.HasForeignKey(e => e.SubcategoryId)
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.HasOne(e => e.Category)
				.WithMany()
				.HasForeignKey(e => e.CategoryId)
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.HasOne(e => e.Promotion)
				.WithOne()
				.HasForeignKey<Promotion>(e => e.ProductId);

			builder
				.Property(e => e.Created)
				.HasDefaultValueSql("getdate()");

			builder
				.Property(e => e.Updated)
				.ValueGeneratedOnAddOrUpdate();

			builder
				.Property(e => e.Price)
				.HasColumnType("decimal(9,2)");

			builder
				.Property(e => e.ImageUrl)
				.IsUnicode(false);

			builder
				.HasQueryFilter(e => e.SoftDeleted);
		}
	}
}
