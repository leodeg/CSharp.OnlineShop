using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShop.Data.Models
{
	public class Order : Model
	{
		[Required]
		public double TotalPrice { get; set; }
		[Required]
		public DateTime OrderDate { get; set; }
		public DateTime? ProcessedDate { get; set; }
		public bool IsProcessed { get; set; }


		[Required]
		[ForeignKey(nameof(Customer))]
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; }
	}
}
