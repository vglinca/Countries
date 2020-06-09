using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Core.Infrastructure
{
	public class SortingArguments
	{
		public string OrderBy { get; set; } = Constants.OrderBy;
		public string Direction { get; set; } = Constants.OrderByDirection;
	}
}
