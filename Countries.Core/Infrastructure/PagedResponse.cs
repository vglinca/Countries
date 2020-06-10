using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Core.Infrastructure
{
	public class PagedResponse<T> where T : class
	{
		public PageData PageData { get; set; }
		public IList<T> Items { get; set; }
	}
}
