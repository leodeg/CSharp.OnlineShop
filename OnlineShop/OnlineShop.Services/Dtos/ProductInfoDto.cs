using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using OnlineShop.Data.Models;

namespace OnlineShop.Services.Dtos
{
	public class ProductInfoDto : Model
	{
		[Required]
		[MaxLength(100)]
		public string Title { get; set; }

		[Required]
		public double Price { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string Description { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string ShortDescription { get; set; }

		[DataType(DataType.ImageUrl)]
		public string ImageUrl { get; set; }

		[Required]
		public int Quantity { get; set; }

		[Required]
		[ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }
		public Category Category { get; set; }

		[Required]
		[ForeignKey(nameof(Subcategory))]
		public int SubcategoryId { get; set; }
		public Subcategory Subcategory { get; set; }
	}
}
