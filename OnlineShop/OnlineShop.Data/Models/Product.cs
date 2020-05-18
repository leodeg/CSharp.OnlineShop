using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShop.Data.Models
{
	public class Product : Model
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
		[DataType(DataType.DateTime)]
		public DateTime Created { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime Updated { get; set; }


		[ForeignKey(nameof(Promotion))]
		public int PromotionId { get; set; }
		public Promotion Promotion { get; set; }

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
