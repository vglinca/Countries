using Countries.Api.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces(ApiConstants.ApplicationJson, ApiConstants.ApplicationHateoasJson)]
	public class BaseController : ControllerBase
	{
		protected readonly IMediator _mediator;
		public BaseController(IMediator mediator)
		{
			_mediator = mediator;
		}
	}
}
