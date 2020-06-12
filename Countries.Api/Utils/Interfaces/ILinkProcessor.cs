using Countries.Api.Models.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries.Api.Utils.Interfaces
{
	public interface ILinkProcessor
	{
		string ProcessLinksForCollection(ResourceParameters resourceParameters);
	}
}
