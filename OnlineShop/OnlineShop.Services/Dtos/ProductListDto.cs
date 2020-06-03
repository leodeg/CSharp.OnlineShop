using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using OnlineShop.Data.Models;

namespace OnlineShop.Services.Dtos
{

	public class ProductListDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
		public double ActualPrice { get; set; }
		public string PromotionMessage { get; set; }
		public string ShortDescription { get; set; }
		public string ImageUrl { get; set; }

		public DateTime PublishedOn { get; set; }

		public Category Category { get; set; }
		public Subcategory Subcategory { get; set; }
	}
}
