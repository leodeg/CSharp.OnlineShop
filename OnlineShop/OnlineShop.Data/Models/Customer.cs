using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShop.Data.Models
{
	public class Customer : Model
	{
		[Required]
		[MaxLength(250)]
		public string FullName { get; set; }
		[Required]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]
		[MaxLength(150)]
		public string Country { get; set; }
		[Required]
		[MaxLength(150)]
		public string City { get; set; }
		[Required]
		[MaxLength(250)]
		public string PostAddress { get; set; }
		[Required]
		[MaxLength(50)]
		public string ZipCode { get; set; }
	}
}
