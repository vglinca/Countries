using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Countries.Api.Models.Countries;
using Countries.Api.Models.Links;
using Countries.Api.Utils;
using Countries.Core.Infrastructure;
using Countries.Core.Repository.Interfaces;
using Countries.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;

namespace Countries.Api.Logic.Countries.Queries
{
	public class GetCountriesQuery : IRequest<PagedResponse<CountryModel>>
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

	public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, PagedResponse<CountryModel>>
	{
		private readonly IGenericRepository _countriesRepository;
		private readonly IMapper _mapper;
		private readonly HostUri _url;

		public GetCountriesQueryHandler(IGenericRepository countriesRepository, IMapper mapper, IOptions<HostUri> uriOptions)
		{
			_countriesRepository = countriesRepository;
			_mapper = mapper;
			_url = uriOptions.Value;
		}
		public async Task<PagedResponse<CountryModel>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
		{
			var filterArgs = new List<FilterArguments> { request.FilterArgs };
			var pagedCountries = await _countriesRepository.GetAllAsync<Country>(request.PageArgs, request.SortingArgs, filterArgs, LogicalOperator.Or);
			var models = _mapper.Map<List<CountryModel>>(pagedCountries.Items);
			
			var pagedResponse = new PagedResponse<CountryModel>
			{
				PageData = pagedCountries.PageData,
				Items = models
			};
			return pagedResponse;
		}

		private IEnumerable<LinkModel> CreateLinksForCountry(PageArguments pageData, SortingArguments sortArgs, FilterArguments filter)
		{
			var links = new List<LinkModel>();
			links.Add(new LinkModel { Href = $"{_url}/api/countries/all?pageIndex={pageData.PageIndex}&pageSize={pageData.PageSize}&orderBy={sortArgs.OrderBy}&direction={sortArgs.Direction}" });
			return links;
		}
	}
}
