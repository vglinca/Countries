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
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Net;
using Microsoft.AspNetCore.Http;
using Countries.Api.Utils;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Web;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Countries.Api.Utils.Interfaces;
using System.Net.Http.Headers;

namespace Countries.Api.Controllers
{
	public class CountriesController : BaseController
	{
		
		public CountriesController(IMediator mediator, ILinkProcessor processor) : base(mediator, processor)
		{}

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<CountryModel>>> GetCountries([FromQuery] PageArguments pageArgs, 
			[FromQuery] SortingArguments sortingArgs, [FromQuery] FilterArguments filterArgs,
			[FromHeader(Name = ApiConstants.AcceptHeader)] string mediaType)
		{
			if(!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
			{
				return BadRequest();
			}
			var pagedCountries = await _mediator.Send(new GetCountriesQuery(pageArgs, sortingArgs, new List<FilterArguments> { filterArgs }));
			
			var links = DecorateResponse(pagedCountries.PageData, sortingArgs, filterArgs, Request.Path);
			
			return parsedMediaType.MediaType.Contains(ApiConstants.HateoasKeyword) ? 
				Ok(new { pagedCountries.Items, links }) : Ok(pagedCountries.Items);
		}

		[HttpGet("currency/{currency}")]
		public async Task<ActionResult<IEnumerable<CountryModel>>> GetCountriesByCurrency(string currency,
			[FromQuery] PageArguments pageArgs, [FromQuery] SortingArguments sortingArgs,
			[FromHeader(Name = ApiConstants.AcceptHeader)] string mediaType)
		{
			var pagedCountries = await _mediator.Send(new GetCountriesQuery(pageArgs, sortingArgs, new List<FilterArguments>
			{
				new FilterArguments { FilterProperty = "Currency.Code", FilterValues = new string[] { currency } }
			}));

			return Ok(pagedCountries.Items);
		}

		[HttpGet("alpha")]
		public async Task<ActionResult<IEnumerable<CountryModel>>> GetCountriesByAlphaCodes([FromQuery] string codes,
			[FromQuery] PageArguments pageArgs, [FromQuery] SortingArguments sortingArgs)
		{
			var filterArgs = new List<FilterArguments>
			{
				new FilterArguments { FilterProperty = nameof(Country.Alpha2Code), FilterValues = codes.Split(';') },
				new FilterArguments { FilterProperty = nameof(Country.Alpha3Code), FilterValues = codes.Split(';') }
			};

			var pagedCountries = await _mediator.Send(new GetCountriesQuery(pageArgs, sortingArgs, filterArgs));
			return Ok(pagedCountries.Items);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CountryModel>> GetCountry(long id)
		{
			var country = await _mediator.Send(new GetCountryQuery(id));
			return Ok(country);
		}

		[HttpGet("alpha/{code}")]
		public async Task<ActionResult<CountryModel>> GetCountryByAlphaCode(string code,
			[FromQuery] PageArguments pageArgs, [FromQuery] SortingArguments sortingArgs)
		{
			Expression<Func<Country, bool>> expression = c => c.Alpha2Code == code || c.Alpha3Code == code;
			var predicates = new List<Expression<Func<Country, bool>>>();
			predicates.Add(expression);
			var country = await _mediator.Send(new GetCountriesWithPredicateQuery(predicates));

			return Ok(country);
		}

		[HttpGet("capital/{capital}")]
		public async Task<ActionResult<CountryModel>> GetCountryByCapital(string capital)
		{
			Expression<Func<Country, bool>> expression = c => c.Capital == capital;
			var predicates = new List<Expression<Func<Country, bool>>>();
			predicates.Add(expression);
			var countries = await _mediator.Send(new GetCountriesWithPredicateQuery(predicates));
			return Ok(countries);
		}

		[HttpGet("lang/{lang}")]
		public async Task<ActionResult<IEnumerable<CountryModel>>> GetCountriesByLanguage(string lang)
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
			return CreatedAtAction(nameof(GetCountry), new { id}, model);
		}

		[HttpPost("collection")]
		public async Task<IActionResult> AddCountries([FromBody] List<CountryForCreationModel> models)
		{
			await _mediator.Send(new AddCountriesCollectionCommand(models));
			return Ok();
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
