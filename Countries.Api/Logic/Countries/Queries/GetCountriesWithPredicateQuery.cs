using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Countries.Api.Models.Countries;
using Countries.Core.Repository.Interfaces;
using Countries.Domain.Entities;
using MediatR;

namespace Countries.Api.Logic.Countries.Queries
{
	public class GetCountriesWithPredicateQuery : IRequest<IEnumerable<CountryModel>>
	{
		public List<Expression<Func<Country, bool>>> Predicates { get; } = new List<Expression<Func<Country, bool>>>();

		public GetCountriesWithPredicateQuery(List<Expression<Func<Country, bool>>> predicates)
		{
			Predicates = predicates;
		}
	}

	public class GetCountriesWithPredicateQueryHandler: IRequestHandler<GetCountriesWithPredicateQuery, IEnumerable<CountryModel>>
	{
		private readonly IGenericRepository _countriesRepository;
		private readonly IMapper _mapper;

		public GetCountriesWithPredicateQueryHandler(IGenericRepository countriesRepository, IMapper mapper)
		{
			_countriesRepository = countriesRepository;
			_mapper = mapper;
		}
		public async Task<IEnumerable<CountryModel>> Handle(GetCountriesWithPredicateQuery request, CancellationToken cancellationToken)
		{
			var countries = await _countriesRepository.GetListWithPredicateAsync<Country>(request.Predicates);
			var models = _mapper.Map<IEnumerable<CountryModel>>(countries);
			return models;
		}
	}
}
