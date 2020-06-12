using Countries.Api.Models.Links;
using Countries.Api.Utils.Interfaces;
using Countries.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries.Api.Utils
{
	public class LinksCreator
	{
		public static IEnumerable<LinkModel> CreateLinksForCountries(PageData pageData, SortingArguments sortArgs, 
			FilterArguments filter, string path, ILinkProcessor processor)
		{
			var links = new List<LinkModel>();
			if (pageData.HasPrevPage)
			{
				links.Add(new LinkModel
				{
					Href = processor.ProcessLinksForCollection(new ResourceParameters
					{
						PageArgs = new PageArguments
						{
							PageIndex = pageData.PageIndex,
							PageSize = pageData.PageSize
						},
						SortArgs = sortArgs,
						FilterArgs = filter,
						Path = path,
						LinkType = LinkType.Previous
					}),
					Method = "GET",
					Rel = "prev_page"
				});
			}

			if (pageData.HasNextPage)
			{
				links.Add(new LinkModel
				{
					Href = processor.ProcessLinksForCollection(new ResourceParameters
					{
						PageArgs = new PageArguments
						{
							PageIndex = pageData.PageIndex,
							PageSize = pageData.PageSize
						},
						SortArgs = sortArgs,
						FilterArgs = filter,
						Path = path,
						LinkType = LinkType.Next
					}),
					Method = "GET",
					Rel = "prev_page"
				});
			}

			links.Add(new LinkModel
			{
				Href = processor.ProcessLinksForCollection(new ResourceParameters
				{
					PageArgs = new PageArguments
					{
						PageIndex = pageData.PageIndex,
						PageSize = pageData.PageSize
					},
					SortArgs = sortArgs,
					FilterArgs = filter,
					Path = path,
					LinkType = LinkType.Current
				}),
				Method = "GET",
				Rel = "self"
			});

			
			return links;
		}
	}
}
