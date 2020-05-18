using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShop.Data.Models
{
	public class Promotion : Model
	{
		[DataType(DataType.DateTime)]
		public DateTime Created { get; set; }

		public double PriceOffer { get; set; }

		[MaxLength(200)]
		public string Message { get; set; }

		public int ProductId { get; set; }
	}
}
