using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineShop.Services.ProductServices.QueryExtensions;

namespace OnlineShop.Services.ProductServices
{
	public class ProductFilterPageOptions
	{
		public const int DefaultPageSize = 10;

		public string SearchText { get; set; }

		public int CurrentPage { get; set; } = 1;
		public int PageSize { get; set; } = DefaultPageSize;

		public double MinPrice { get; set; }
		public double MaxPrice { get; set; }

		public int CategoryId { get; set; }
		public int SubcategoryId { get; set; }

		public ProductOrderBy OrderBy { get; set; }
		public ProductFilterBy FilterBy { get; set; }


		public int[] PageSizes = new[] { 5, DefaultPageSize, 20, 50, 100, 500, 750, 1000 };
		public int PagesCount { get; set; }
		public string PreviousState { get; set; }
		public int TotalItemsCount { get; set; }


		/// <summary>
		/// Calculate pages count and reset current page if options is changed.
		/// </summary>
		public void UpdateOptionsProperties<T> (IQueryable<T> query)
		{
			TotalItemsCount = query.Count();
			PagesCount = (int)Math.Ceiling((double)query.Count() / PageSize);
			CurrentPage = Math.Min(Math.Max(1, CurrentPage), PagesCount);
			ResetCurrentPageNumberOnStateChanges();
		}

		private void ResetCurrentPageNumberOnStateChanges()
		{
			var newState = GenerateCheckState();
			if (PreviousState != newState)
				CurrentPage = 1;
			PreviousState = newState;
		}

		/// <summary>
		/// Generate state point with the current filters.
		/// If filters is changed current page number should be set back to 0.
		/// </summary>
		private string GenerateCheckState()
		{
			return $"{(int)FilterBy},{MinPrice},{MaxPrice},{PageSize},{PagesCount},{SearchText}";
		}
	}
}
