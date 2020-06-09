using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShop.Data.Models
{
	public class Subcategory : Model
	{
		[Required]
		[MaxLength(100)]
		public string Title { get; set; }

		[ForeignKey(nameof(Category))]
		public int CategoryId { get; set; }
	}
}
