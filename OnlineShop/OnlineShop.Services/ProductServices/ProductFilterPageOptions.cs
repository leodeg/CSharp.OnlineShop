using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineShop.Services.ProductServices.QueryObjects;

namespace OnlineShop.Services.ProductServices
{
	public class ProductFilterPageOptions
	{
		public const int DefaultPageSize = 10;

		public int PageNumber { get; set; }
		public int PageSize { get; set; }

		public double MinPrice { get; set; }
		public double MaxPrice { get; set; }

		public int CategoryId { get; set; }
		public int SubcategoryId { get; set; }

		public ProductOrderBy OrderBy { get; set; }
		public ProductFilterBy FilterBy { get; set; }


		public int[] PageSizes = new[] { 5, DefaultPageSize, 20, 50, 100, 500, 1000 };
		public int PagesCount { get; set; }
		public string PreviousState { get; set; }


		public void Setup<T> (IQueryable<T> query)
		{
			PagesCount = (int)Math.Ceiling((double)query.Count() / PageSize);
			PageNumber = Math.Min(Math.Max(1, PageNumber), PagesCount);
			ResetCurrentPageNumberOnStateChanges();
		}

		private void ResetCurrentPageNumberOnStateChanges()
		{
			var newState = GenerateCheckState();
			if (!PreviousState.Equals(newState))
				PageNumber = 1;
			PreviousState = newState;
		}

		/// <summary>
		/// Generate state point with the current filters.
		/// If filters is changed current page number should be set back to 0.
		/// </summary>
		private string GenerateCheckState()
		{
			return $"{(int)FilterBy},{MinPrice},{MaxPrice},{PageSize},{PagesCount}";
		}
	}
}
