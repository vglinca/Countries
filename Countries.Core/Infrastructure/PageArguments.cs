using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Countries.Core.Infrastructure
{
	public class PageArguments
	{
		private const int maxSize = 30;
		private int _pageSize = maxSize;

		[Range(1, 1000)]
		public int PageIndex { get; set; } = 1;
		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = (value > maxSize || value < 1) ? maxSize : value;
		}
	}
}
