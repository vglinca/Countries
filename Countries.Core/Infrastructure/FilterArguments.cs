using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Core.Infrastructure
{
	public class FilterArguments
	{
		public string FilterProperty { get; set; } = Constants.DefaultPropertyName;
		public string[] FilterValues { get; set; }

		public FilterArguments()
		{
			FilterValues = new string[] {""};
		}
	}
}
