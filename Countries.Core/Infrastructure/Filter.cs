using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Core.Infrastructure
{
	public class Filter
	{
		public string PropertyName { get; set; }
		public string[] PropertyValues { get; set; }

		public Filter()
		{
		}
	}
}
