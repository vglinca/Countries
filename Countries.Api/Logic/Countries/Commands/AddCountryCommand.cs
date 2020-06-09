using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Countries.Api.Models.Countries;
using Countries.Core.Repository.Interfaces;
using Countries.Domain.Entities;
using MediatR;

namespace Countries.Api.Logic.Countries.Commands
{
	public class AddCountryCommand : IRequest<long>
	{
		public CountryForCreationModel Model { get;}
		public AddCountryCommand(CountryForCreationModel model)
		{
			Model = model;
		}
	}

	public class AddCountryCommandHandler : IRequestHandler<AddCountryCommand, long>
	{
		private readonly IGenericRepository _countriesRepository;
		private readonly IMapper _mapper;

		public AddCountryCommandHandler(IGenericRepository countriesRepository, IMapper mapper)
		{
			_countriesRepository = countriesRepository;
			_mapper = mapper;
		}
		public async Task<long> Handle(AddCountryCommand request, CancellationToken cancellationToken)
		{
			var country = _mapper.Map<Country>(request.Model);
			var createdEntity = await _countriesRepository.CreateAsync<Country>(country);
			return createdEntity.Id;
		}
	}
}
