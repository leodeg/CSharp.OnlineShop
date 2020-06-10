using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShop.Data.Models
{
	public class OrderItem : Model
	{
		[Required]
		[ForeignKey(nameof(Product))]
		public int ProductId { get; set; }
		public Product Product { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public int OrderId { get; set; }
	}
}
