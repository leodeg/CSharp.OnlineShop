using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OnlineShop.Models;

namespace OnlineShop.TagHelpers
{
	[HtmlTargetElement("div", Attributes = "paging-information")]
	public class PaginationTagHelper : TagHelper
	{
		private IUrlHelperFactory urlHelperFactory;

		public PaginationTagHelper(IUrlHelperFactory helperFactory)
		{
			urlHelperFactory = helperFactory;
		}

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }
		public PagingInformation PagingInformation { get; set; }
		public string PageAction { get; set; }
		public string UlClasses { get; set; }
		public string OnClickMethod { get; set; }
		public string Name { get; set; }


		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
			output.TagName = "div";

			TagBuilder tag = new TagBuilder("ul");
			tag.AddCssClass("pagination " + UlClasses);

			if (PagingInformation.HasPreviousPage)
			{
				TagBuilder previousPage = CreateTag(PagingInformation.CurrentPage - 1, urlHelper, "Previous");
				TagBuilder firstPage = CreateTag(1, urlHelper, "First Page");
				tag.InnerHtml.AppendHtml(firstPage);
				tag.InnerHtml.AppendHtml(previousPage);
			}

			CreateMiddlePages(urlHelper, tag);

			if (PagingInformation.HasNextPage)
			{
				TagBuilder nextPage = CreateTag(PagingInformation.CurrentPage + 1, urlHelper, "Next");
				TagBuilder lastPage = CreateTag(PagingInformation.TotalPages, urlHelper, "Last Page");
				tag.InnerHtml.AppendHtml(nextPage);
				tag.InnerHtml.AppendHtml(lastPage);
			}

			output.Content.AppendHtml(tag);
		}

		private void CreateMiddlePages(IUrlHelper urlHelper, TagBuilder tag)
		{
			if (PagingInformation.TotalPages <= 6)
			{
				CreatePagination(tag, urlHelper, 1, PagingInformation.TotalPages);
			}
			else if (PagingInformation.TotalPages > 6)
			{
				int leftPages = PagingInformation.TotalPages - PagingInformation.CurrentPage;

				// TODO: create a more elegant solution for pagination
				// Left pagination links (before current page)
				if (PagingInformation.CurrentPage <= 3)
				{
					CreatePagination(tag, urlHelper, 1, 3);
				}
				else
				{
					CreatePagination(tag, urlHelper, PagingInformation.CurrentPage - 3, PagingInformation.CurrentPage);
				}

				// Right pagination links (after current page)
				if (PagingInformation.CurrentPage <= 3)
				{
					CreatePagination(tag, urlHelper, PagingInformation.TotalPages - 2, PagingInformation.TotalPages);
				}
				else if (leftPages > 0 && leftPages <= 2)
				{
					CreatePagination(tag, urlHelper, PagingInformation.CurrentPage + 1, PagingInformation.CurrentPage + leftPages);
				}
				else if (leftPages > 0)
				{
					CreatePagination(tag, urlHelper, PagingInformation.CurrentPage + 1, PagingInformation.CurrentPage + 3);
				}
			}
		}

		private void CreatePagination(TagBuilder tag, IUrlHelper urlHelper, int start, int end)
		{
			for (int i = start; i <= end; i++)
			{
				TagBuilder current = CreateTag(i, urlHelper);
				tag.InnerHtml.AppendHtml(current);
			}
		}

		private TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper, string title = null)
		{
			TagBuilder item = new TagBuilder("li");
			TagBuilder button = new TagBuilder("button");

			if (pageNumber == this.PagingInformation.CurrentPage)
				item.AddCssClass("active");
			item.AddCssClass("page-item");

			button.AddCssClass("page-link");
			button.Attributes["name"] = Name;
			button.Attributes["onclick"] = OnClickMethod + "(" + pageNumber.ToString() + ")";
			button.Attributes["value"] = pageNumber.ToString();

			button.InnerHtml.Append(title ?? pageNumber.ToString());
			item.InnerHtml.AppendHtml(button);
			return item;
		}
	}
}
