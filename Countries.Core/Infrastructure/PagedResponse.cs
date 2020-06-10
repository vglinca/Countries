using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Core.Infrastructure
{
	public class PagedResponse<T> where T : class
	{
		private bool _hasPrevPage;
		private bool _hasNextPage;
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int TotalItems { get; set; }

		public bool HasPrevPage 
		{
			get => _hasPrevPage;
			set => _hasPrevPage = (PageIndex > 1);
		}
		public bool HasNextPage
		{
			get => _hasNextPage;
			set => _hasNextPage = (PageIndex < TotalItems / PageSize - 1);
		}

		public IList<T> Items { get; set; }
	}
}
