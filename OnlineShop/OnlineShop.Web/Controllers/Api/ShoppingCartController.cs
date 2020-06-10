using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.AdminServices;
using OnlineShop.Services.OrderServices;

namespace OnlineShop.Web.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShoppingCartController : ControllerBase
	{
		private readonly ShoppingCart shoppingCart;

		public ShoppingCartController(ShoppingCart shoppingCart)
		{
			this.shoppingCart = shoppingCart;
		}

		[HttpPost]
		public IActionResult Post(int productId, int quantity, double price)
		{
			shoppingCart.Add(productId, quantity, price);
			return Ok();
		}

		[HttpDelete("{productId}")]
		public IActionResult Delete(int? productId)
		{
			if (productId == null)
				return BadRequest();

			shoppingCart.Remove(productId.Value);
			return Ok();
		}
	}
}