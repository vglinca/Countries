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
	public class GetCountriesByAlphaCodesQuery : IRequest<IEnumerable<CountryModel>>
	{
		public string[] Codes { get; }
		public GetCountriesByAlphaCodesQuery(string codes)
		{
			Codes = codes.Split(';');
		}
	}

	public class GetCountriesByAlphaCodesHandler : IRequestHandler<GetCountriesByAlphaCodesQuery, IEnumerable<CountryModel>>
	{
		private readonly IGenericRepository _countriesRepository;
		private readonly IMapper _mapper;

		public GetCountriesByAlphaCodesHandler(IGenericRepository countriesRepository, IMapper mapper)
		{
			_countriesRepository = countriesRepository;
			_mapper = mapper;
		}
		public async Task<IEnumerable<CountryModel>> Handle(GetCountriesByAlphaCodesQuery request, CancellationToken cancellationToken)
		{
			var filters = new List<FilterArguments> {
				new FilterArguments{FilterProperty = $"{nameof(Country.Alpha2Code)}", FilterValues = request.Codes},
				new FilterArguments{FilterProperty = $"{nameof(Country.Alpha3Code)}", FilterValues = request.Codes},
			};

			var countries = await _countriesRepository.GetListUsingFilters<Country>(filters, LogicalOperator.Or);
			var models = _mapper.Map<IEnumerable<CountryModel>>(countries);
			return models;
		}
	}
}
