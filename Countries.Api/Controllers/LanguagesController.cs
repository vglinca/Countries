using Countries.Api.Logic.Languages.Queries;
using Countries.Api.Utils.Interfaces;
using Countries.Core.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Countries.Api.Controllers
{
	public class LanguagesController : BaseController
	{
		public LanguagesController(IMediator mediator, ILinkProcessor processor) : base(mediator, processor)
		{}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] PageArguments pageArgs,
			[FromQuery] SortingArguments sortingArgs, [FromQuery] FilterArguments filterArgs)
		{
			var languages = await _mediator.Send(new GetLanguagesQuery(pageArgs, sortingArgs, filterArgs));
			return Ok(languages);
		}
	}
}
