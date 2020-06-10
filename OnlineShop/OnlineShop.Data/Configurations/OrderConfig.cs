using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Configurations
{

	internal class OrderConfig : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder
				.HasMany(e => e.OrderItems)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne(e => e.Customer)
				.WithMany()
				.HasForeignKey(e => e.CustomerId)
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.Property(e => e.OrderDate)
				.HasDefaultValueSql("getdate()")
				.ValueGeneratedOnAdd();
		}
	}
}
