using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Configurations
{
	internal class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder
				.HasOne(e => e.Product)
				.WithMany()
				.HasForeignKey(e => e.ProductId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
