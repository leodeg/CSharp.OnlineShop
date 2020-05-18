﻿using System;
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

		public double NewPrice { get; set; }

		[MaxLength(100)]
		public string Message { get; set; }
	}
}
