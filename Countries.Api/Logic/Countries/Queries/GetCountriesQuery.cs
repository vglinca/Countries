using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Countries.Api.Models.Countries;
using Countries.Core.Infrastructure;
using Countries.Core.Repository.Interfaces;
using Countries.Domain.Entities;
using MediatR;

namespace Countries.Api.Logic.Countries.Queries
{
	public class GetCountriesQuery : IRequest<IEnumerable<CountryModel>>
	{
		public PageArguments PageArgs { get; }
		public SortingArguments SortingArgs { get; }
		public FilterArguments FilterArgs { get; set; }

		public GetCountriesQuery(PageArguments pageArgs, SortingArguments sortingArgs, FilterArguments filterArgs)
		{
			PageArgs = pageArgs;
			SortingArgs = sortingArgs;
			FilterArgs = filterArgs;
		}
	}

	public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IEnumerable<CountryModel>>
	{
		private readonly IGenericRepository _countriesRepository;
		private readonly IMapper _mapper;

		public GetCountriesQueryHandler(IGenericRepository countriesRepository, IMapper mapper)
		{
			_countriesRepository = countriesRepository;
			_mapper = mapper;
		}
		public async Task<IEnumerable<CountryModel>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
		{
			var filterArgs = new List<FilterArguments> { request.FilterArgs };
			var pagedCountries = await _countriesRepository.GetAllAsync<Country>(request.PageArgs, 
				request.SortingArgs, filterArgs, LogicalOperator.Or);
			var models = _mapper.Map<List<CountryModel>>(pagedCountries.Items);
			return models;
		}
	}
}
