using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShop.Data.Models
{
	public class Category : Model
	{
		[MaxLength(100)]
		public string Title { get; set; }

		public IEnumerable<Subcategory> Subcategories { get; set; }
	}
}
