using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Countries.Api.Models.Countries;
using Countries.Core.Repository.Interfaces;
using Countries.Domain.Entities;
using MediatR;

namespace Countries.Api.Logic.Countries.Queries
{
	public class GetCountryQuery : IRequest<CountryModel>
	{
		public long Id { get;}
		public GetCountryQuery(long id)
		{
			Id = id;
		}
	}

	public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, CountryModel>
	{
		private readonly IGenericRepository _countriesRepository;
		private readonly IMapper _mapper;

		public GetCountryQueryHandler(IGenericRepository countriesRepository, IMapper mapper)
		{
			_countriesRepository = countriesRepository;
			_mapper = mapper;
		}
		public async Task<CountryModel> Handle(GetCountryQuery request, CancellationToken cancellationToken)
		{
			var country = await _countriesRepository.GetOneAsync<Country>(request.Id);
			var model = _mapper.Map<CountryModel>(country);
			return model;
		}
	}
}
