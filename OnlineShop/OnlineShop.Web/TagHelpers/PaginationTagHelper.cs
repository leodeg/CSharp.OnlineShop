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
		public PagingInformation PagingInformation { get; set; }

		public string PageAction { get; set; }
		public string UlClasses { get; set; }
		public string OnClickMethod { get; set; }
		public string Name { get; set; }

		private const int PageItemsAmount = 6;
		private int MiddlePage => PageItemsAmount / 2;

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			CreatePaginationUlList(output);
		}

		private void CreatePaginationUlList(TagHelperOutput output)
		{
			output.TagName = "div";
			TagBuilder tag = new TagBuilder("ul");
			tag.AddCssClass("pagination " + UlClasses);

			CreatePreviousPaginationItems(tag);
			CreateMiddlePaginationItems(tag);
			CreateNextPaginationItems(tag);

			output.Content.AppendHtml(tag);
		}

		private void CreatePreviousPaginationItems(TagBuilder tag)
		{
			if (PagingInformation.HasPreviousPage)
			{
				TagBuilder firstPage = CreatePaginationItem(1, "First Page");
				tag.InnerHtml.AppendHtml(firstPage);
				TagBuilder previousPage = CreatePaginationItem(PagingInformation.CurrentPage - 1, "Previous");
				tag.InnerHtml.AppendHtml(previousPage);
			}
		}

		private void CreateNextPaginationItems(TagBuilder tag)
		{
			if (PagingInformation.HasNextPage)
			{
				TagBuilder nextPage = CreatePaginationItem(PagingInformation.CurrentPage + 1, "Next");
				tag.InnerHtml.AppendHtml(nextPage);
				TagBuilder lastPage = CreatePaginationItem(PagingInformation.TotalPages, "Last Page");
				tag.InnerHtml.AppendHtml(lastPage);
			}
		}

		private void CreateMiddlePaginationItems(TagBuilder parentTag)
		{
			if (PagingInformation.TotalPages <= PageItemsAmount)
			{
				CreatePaginationItems(parentTag, 1, PagingInformation.TotalPages);
			}
			else if (PagingInformation.TotalPages > PageItemsAmount)
			{
				CreateLeftFromActivePage(parentTag);
				CreateRightFromActivePage(parentTag);
			}
		}

		private void CreateLeftFromActivePage(TagBuilder parentTag)
		{
			if (PagingInformation.CurrentPage <= MiddlePage)
				CreatePaginationItems(parentTag, 1, MiddlePage);
			else CreatePaginationItems(parentTag, PagingInformation.CurrentPage - MiddlePage, PagingInformation.CurrentPage);
		}

		private void CreateRightFromActivePage(TagBuilder parentTag)
		{
			int remainingPages = PagingInformation.TotalPages - PagingInformation.CurrentPage;

			if (PagingInformation.CurrentPage <= MiddlePage)
				CreatePaginationItems(parentTag, PagingInformation.TotalPages - (MiddlePage - 1), PagingInformation.TotalPages);
			else if (remainingPages > 0 && remainingPages <= (MiddlePage - 1))
				CreatePaginationItems(parentTag, PagingInformation.CurrentPage + 1, PagingInformation.CurrentPage + remainingPages);
			else if (remainingPages > 0)
				CreatePaginationItems(parentTag, PagingInformation.CurrentPage + 1, PagingInformation.CurrentPage + MiddlePage);
		}

		private void CreatePaginationItems(TagBuilder parentTag, int startPage, int endPage)
		{
			for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
			{
				TagBuilder current = CreatePaginationItem(pageNumber);
				parentTag.InnerHtml.AppendHtml(current);
			}
		}

		private TagBuilder CreatePaginationItem(int pageNumber, string buttonTitle = null)
		{
			TagBuilder paginationItem = new TagBuilder("li");
			if (pageNumber == this.PagingInformation.CurrentPage)
				paginationItem.AddCssClass("active");
			paginationItem.AddCssClass("page-item");
			paginationItem.InnerHtml.AppendHtml(CreatePageLinkButton(pageNumber, buttonTitle));
			return paginationItem;
		}

		private TagBuilder CreatePageLinkButton(int pageNumber, string title)
		{
			TagBuilder button = new TagBuilder("button");
			button.AddCssClass("page-link");
			button.Attributes["value"] = pageNumber.ToString();

			if (!string.IsNullOrEmpty(Name))
				button.Attributes["name"] = Name;

			if (!string.IsNullOrEmpty(OnClickMethod))
				button.Attributes["onclick"] = OnClickMethod + "(" + pageNumber.ToString() + ")";
			button.InnerHtml.Append(title ?? pageNumber.ToString());
			return button;
		}
	}
}
