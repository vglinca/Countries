using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries.Api.Models.Links
{
	public class LinkResponseModel<T> where T : class
	{
		public IList<T> Data { get; set; }
		public IEnumerable<LinkModel> Links { get; set; }
	}
}
