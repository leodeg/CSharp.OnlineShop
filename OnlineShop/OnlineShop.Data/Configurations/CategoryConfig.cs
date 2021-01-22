using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Configurations
{
	internal class CategoryConfig : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder
				.HasMany(e => e.Subcategories)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
