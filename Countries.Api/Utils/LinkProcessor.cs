using Countries.Api.Models.Links;
using Countries.Api.Utils.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Api.Utils
{
	public class LinkProcessor : ILinkProcessor
	{
		private readonly HostUri _hostUri;
		public LinkProcessor(IOptions<HostUri> options)
		{
			_hostUri = options.Value;
		}
		public string ProcessLinksForCollection(ResourceParameters resourceParameters)
		{
			return resourceParameters.LinkType switch
			{
				LinkType.Previous => new StringBuilder(_hostUri.Url)
					.Append($"{resourceParameters.Path}")
					.Append($"?{nameof(resourceParameters.PageArgs.PageIndex)}={resourceParameters.PageArgs.PageIndex - 1}")
					.Append($"&{nameof(resourceParameters.PageArgs.PageSize)}={resourceParameters.PageArgs.PageSize}")
					.Append($"&{nameof(resourceParameters.SortArgs.OrderBy)}={resourceParameters.SortArgs.OrderBy}")
					.Append($"&{nameof(resourceParameters.SortArgs.Direction)}={resourceParameters.SortArgs.Direction}")
					.Append($"{(string.IsNullOrWhiteSpace(resourceParameters.FilterArgs.FilterValues[0]) ? "" : $"&{nameof(resourceParameters.FilterArgs.FilterProperty)}={resourceParameters.FilterArgs.FilterProperty}")}")
					.Append($"{(string.IsNullOrWhiteSpace(resourceParameters.FilterArgs.FilterValues[0]) ? "" : $"&{nameof(resourceParameters.FilterArgs.FilterValues)}={string.Join(';', resourceParameters.FilterArgs.FilterProperty)}")}")
					.ToString(),

				LinkType.Next => new StringBuilder(_hostUri.Url)
					.Append($"{resourceParameters.Path}")
					.Append($"?{nameof(resourceParameters.PageArgs.PageIndex)}={resourceParameters.PageArgs.PageIndex + 1}")
					.Append($"&{nameof(resourceParameters.PageArgs.PageSize)}={resourceParameters.PageArgs.PageSize}")
					.Append($"&{nameof(resourceParameters.SortArgs.OrderBy)}={resourceParameters.SortArgs.OrderBy}")
					.Append($"&{nameof(resourceParameters.SortArgs.Direction)}={resourceParameters.SortArgs.Direction}")
					.Append($"{(string.IsNullOrWhiteSpace(resourceParameters.FilterArgs.FilterValues[0]) ? "" : $"&{nameof(resourceParameters.FilterArgs.FilterProperty)}={resourceParameters.FilterArgs.FilterProperty}")}")
					.Append($"{(string.IsNullOrWhiteSpace(resourceParameters.FilterArgs.FilterValues[0]) ? "" : $"&{nameof(resourceParameters.FilterArgs.FilterValues)}={string.Join(';', resourceParameters.FilterArgs.FilterProperty)}")}")
					.ToString(),

				_ => new StringBuilder(_hostUri.Url)
					.Append($"{resourceParameters.Path}")
					.Append($"?{nameof(resourceParameters.PageArgs.PageIndex)}={resourceParameters.PageArgs.PageIndex}")
					.Append($"&{nameof(resourceParameters.PageArgs.PageSize)}={resourceParameters.PageArgs.PageSize}")
					.Append($"&{nameof(resourceParameters.SortArgs.OrderBy)}={resourceParameters.SortArgs.OrderBy}")
					.Append($"&{nameof(resourceParameters.SortArgs.Direction)}={resourceParameters.SortArgs.Direction}")
					.Append($"{(string.IsNullOrWhiteSpace(resourceParameters.FilterArgs.FilterValues[0]) ? "" : $"&{nameof(resourceParameters.FilterArgs.FilterProperty)}={resourceParameters.FilterArgs.FilterProperty}")}")
					.Append($"{(string.IsNullOrWhiteSpace(resourceParameters.FilterArgs.FilterValues[0]) ? "" : $"&{nameof(resourceParameters.FilterArgs.FilterValues)}={string.Join(';', resourceParameters.FilterArgs.FilterProperty)}")}")
					.ToString(),
			};
		}
	}
}
