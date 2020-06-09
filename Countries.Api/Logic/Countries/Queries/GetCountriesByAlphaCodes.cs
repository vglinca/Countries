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
	public class GetCountriesByAlphaCodes : IRequest<IEnumerable<CountryModel>>
	{
		public string[] Codes { get; }
		public GetCountriesByAlphaCodes(string codes)
		{
			Codes = codes.Split(';');
		}
	}

	public class GetCountriesByAlphaCodesHandler : IRequestHandler<GetCountriesByAlphaCodes, IEnumerable<CountryModel>>
	{
		private readonly IGenericRepository _countriesRepository;
		private readonly IMapper _mapper;

		public GetCountriesByAlphaCodesHandler(IGenericRepository countriesRepository, IMapper mapper)
		{
			_countriesRepository = countriesRepository;
			_mapper = mapper;
		}
		public async Task<IEnumerable<CountryModel>> Handle(GetCountriesByAlphaCodes request, CancellationToken cancellationToken)
		{
			var filters = new List<Filter> {
				new Filter{PropertyName = $"{nameof(Country.Alpha2Code)}", PropertyValues = request.Codes},
				new Filter{PropertyName = $"{nameof(Country.Alpha3Code)}", PropertyValues = request.Codes},
			};

			var countries = await _countriesRepository.GetListUsingFilters<Country>(filters, LogicalOperator.Or);
			var models = _mapper.Map<IEnumerable<CountryModel>>(countries);
			return models;
		}
	}
}
