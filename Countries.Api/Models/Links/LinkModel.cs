using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries.Api.Models.Links
{
	public class LinkModel
	{
		public string Href { get; set; }
		public string Rel { get; set; }
		public string Method { get; set; }
	}
}
