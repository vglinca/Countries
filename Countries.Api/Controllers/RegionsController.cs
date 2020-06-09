using Countries.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Countries.Api.Logic.Countries.Queries;
using Countries.Api.Logic.Regions.Queries;

namespace Countries.Api.Controllers
{
	public class RegionsController : BaseController
	{
		public RegionsController(IMediator mediator) : base(mediator)
		{}

		public async Task<IActionResult> GetRegions()
		{
			var regions = await _mediator.Send(new GetRegionsQuery());
			return Ok(regions);
		}

		[HttpGet("{region}/countries")]
		public async Task<IActionResult> GetCountriesByRegion(string region)
		{
			Expression<Func<Country, bool>> expression = c => c.Region.Name == region;
			var predicates = new List<Expression<Func<Country, bool>>>();
			predicates.Add(expression);
			var countries = await _mediator.Send(new GetCountriesWithPredicateQuery(predicates));
			return Ok(countries);
		}
	}
}
