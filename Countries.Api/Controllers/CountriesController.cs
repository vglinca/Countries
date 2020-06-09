using Countries.Api.Models.Countries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Countries.Api.Logic.Countries.Commands;
using Countries.Api.Logic.Countries.Queries;
using Countries.Domain.Entities;
using Countries.Core.Infrastructure;

namespace Countries.Api.Controllers
{
	public class CountriesController : BaseController
	{
		public CountriesController(IMediator mediator) : base(mediator)
		{}

		[HttpGet("all")]
		public async Task<IActionResult> GetCountries([FromQuery] PageArguments pageArgs, 
			[FromQuery] SortingArguments sortingArgs, [FromQuery] FilterArguments filterArgs)
		{
			var countries = await _mediator.Send(new GetCountriesQuery(pageArgs, sortingArgs, filterArgs));
			return Ok(countries);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCountry(long id)
		{
			var country = await _mediator.Send(new GetCountryQuery(id));
			return Ok(country);
		}

		[HttpGet("alpha/{code}")]
		public async Task<IActionResult> GetCountryByAlphaCode(string code)
		{
			Expression<Func<Country, bool>> expression = c => c.Alpha2Code == code || c.Alpha3Code == code;
			var predicates = new List<Expression<Func<Country, bool>>>();
			predicates.Add(expression);
			var countries = await _mediator.Send(new GetCountriesWithPredicateQuery(predicates));
			return Ok(countries);
		}

		[HttpGet("alpha")]
		public async Task<IActionResult> GetCountriesByAlphaCodes([FromQuery] string codes)
		{
			var countries = await _mediator.Send(new GetCountriesByAlphaCodesQuery(codes));
			return Ok(countries);
		}

		[HttpGet("currency/{currency}")]
		public async Task<IActionResult> GetCountryByCurrency(string currency)
		{
			Expression<Func<Country, bool>> expression = c => c.Currency.Code == currency;
			var predicates = new List<Expression<Func<Country, bool>>>();
			predicates.Add(expression);
			var countries = await _mediator.Send(new GetCountriesWithPredicateQuery(predicates));
			return Ok(countries);
		}

		[HttpGet("capital/{capital}")]
		public async Task<IActionResult> GetCountryByCapital(string capital)
		{
			Expression<Func<Country, bool>> expression = c => c.Capital == capital;
			var predicates = new List<Expression<Func<Country, bool>>>();
			predicates.Add(expression);
			var countries = await _mediator.Send(new GetCountriesWithPredicateQuery(predicates));
			return Ok(countries);
		}

		[HttpGet("lang/{lang}")]
		public async Task<IActionResult> GetCountryByLanguage(string lang)
		{
			Expression<Func<Country, bool>> expression = c => c.CountryLanguages
				.Any(l => l.Language.Iso639_1 == lang || l.Language.Iso639_2 == lang);
			var predicates = new List<Expression<Func<Country, bool>>>();
			predicates.Add(expression);
			var countries = await _mediator.Send(new GetCountriesWithPredicateQuery(predicates));
			return Ok(countries);
		}

		[HttpPost]
		public async Task<IActionResult> AddCountry([FromBody] CountryForCreationModel model)
		{
			var id = await _mediator.Send(new AddCountryCommand(model));
			return CreatedAtRoute(nameof(GetCountry), new { id}, model);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCountry(long id)
		{
			await _mediator.Send(new DeleteCountryCommand(id));
			return NoContent();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCountry(long id, [FromBody] CountryForUpdateModel model) 
		{
			await _mediator.Send(new UpdateCountryCommand(id, model));
			return Ok();
		}
	}
}
