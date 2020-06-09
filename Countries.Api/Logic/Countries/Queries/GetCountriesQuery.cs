using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Countries.Api.Models.Countries;
using Countries.Core.Repository.Interfaces;
using Countries.Domain.Entities;
using MediatR;

namespace Countries.Api.Logic.Countries.Queries
{
	public class GetCountriesQuery : IRequest<IEnumerable<CountryModel>>
	{
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
			var countries = await _countriesRepository.GetAllAsync<Country>();
			var models = _mapper.Map<List<CountryModel>>(countries);
			return models;
		}
	}
}
