using Countries.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries.Api.Utils
{
	public class ResourceParameters
	{
		public PageArguments PageArgs { get; set; }
		public SortingArguments SortArgs { get; set; }
		public FilterArguments FilterArgs { get; set; }
		public string Path { get; set; }
		public LinkType LinkType { get; set; }
	}
}
