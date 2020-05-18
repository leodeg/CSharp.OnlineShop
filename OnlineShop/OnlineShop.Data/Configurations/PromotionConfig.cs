using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Configurations
{
	internal class PromotionConfig : IEntityTypeConfiguration<Promotion>
	{
		public void Configure(EntityTypeBuilder<Promotion> builder)
		{
			builder
				.Property(e => e.Created)
				.HasDefaultValueSql("getdate()");

			builder
				.Property(e => e.PriceOffer)
				.HasColumnType("decimal(9,2)");
		}
	}
}
