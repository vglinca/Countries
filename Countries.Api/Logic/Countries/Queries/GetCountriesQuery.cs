﻿using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Countries.Api.Models.Countries;
using Countries.Api.Models.Links;
using Countries.Api.Utils;
using Countries.Api.Utils.Interfaces;
using Countries.Core.Infrastructure;
using Countries.Core.Repository.Interfaces;
using Countries.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Countries.Api.Logic.Countries.Queries
{
	public class GetCountriesQuery : IRequest<PagedResponse<CountryModel>>
	{
		public PageArguments PageArgs { get; }
		public SortingArguments SortingArgs { get; }
		public List<FilterArguments> FilterArgs { get; set; }

		public GetCountriesQuery(PageArguments pageArgs, SortingArguments sortingArgs, List<FilterArguments> filterArgs)
		{
			PageArgs = pageArgs;
			SortingArgs = sortingArgs;
			FilterArgs = filterArgs;
		}
	}

	public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, PagedResponse<CountryModel>>
	{
		private readonly IGenericRepository _countriesRepository;
		private readonly IMapper _mapper;

		public GetCountriesQueryHandler(IGenericRepository countriesRepository, IMapper mapper)
		{
			_countriesRepository = countriesRepository;
			_mapper = mapper;
		}
		public async Task<PagedResponse<CountryModel>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
		{
			//var filterArgs = new List<FilterArguments> { request.FilterArgs };
			var pagedCountries = await _countriesRepository.GetAllAsync<Country>(request.PageArgs, request.SortingArgs, request.FilterArgs, LogicalOperator.Or);
			var models = _mapper.Map<List<CountryModel>>(pagedCountries.Items);
			
			var pagedResponse = new PagedResponse<CountryModel>
			{
				PageData = pagedCountries.PageData,
				Items = models
			};

			return pagedResponse;
		}
	}
}
