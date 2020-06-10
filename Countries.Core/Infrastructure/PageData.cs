using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Core.Infrastructure
{
	public class PageData
	{
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int TotalItems { get; set; }
		public int TotalPages => (int) Math.Ceiling(TotalItems / (double) PageSize);
		public bool HasPrevPage => PageIndex > 1;
		public bool HasNextPage => PageIndex < TotalPages;
	}
}
