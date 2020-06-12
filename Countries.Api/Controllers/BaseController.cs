using Countries.Api.Models.Links;
using Countries.Api.Utils;
using Countries.Api.Utils.Interfaces;
using Countries.Core.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Countries.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces(ApiConstants.ApplicationJson, ApiConstants.ApplicationHateoasJson, 
		ApiConstants.ApplicationXml)]
	//[ResponseCache(CacheProfileName = ApiConstants.CacheProfileName)]
	public class BaseController : ControllerBase
	{
		protected readonly IMediator _mediator;
		protected readonly ILinkProcessor _processor;

		public BaseController(IMediator mediator, ILinkProcessor processor)
		{
			_mediator = mediator;
			_processor = processor;
		}

		[NonAction]
		public IEnumerable<LinkModel> DecorateResponse(PageData pageData, SortingArguments sortArgs, FilterArguments filterArgs, PathString path)
		{
			Response.Headers.Add(Constants.XPagination, JsonSerializer.Serialize(pageData,
				new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));

			var links = LinksCreator.CreateLinksForCountries(pageData, sortArgs, filterArgs, path, _processor);
			return links;
		}
	}
}
