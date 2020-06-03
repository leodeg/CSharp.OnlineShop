using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using OnlineShop.Data.Models;

namespace OnlineShop.Services.Dtos
{
	public class PromotionDto
	{
		[Required]
		public int Id { get; set; }

		public double PriceOffer { get; set; }

		[MaxLength(200)]
		public string Message { get; set; }

		public int ProductId { get; set; }
	}
}
