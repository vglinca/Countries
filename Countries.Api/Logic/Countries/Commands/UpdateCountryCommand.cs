using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Countries.Api.Models.Countries;
using Countries.Core.Repository.Interfaces;
using Countries.Domain.Entities;
using MediatR;

namespace Countries.Api.Logic.Countries.Commands
{
	public class UpdateCountryCommand : IRequest<Unit>
	{
		public long Id { get; }
		public CountryForUpdateModel Model { get; }

		public UpdateCountryCommand(long id, CountryForUpdateModel model)
		{
			Id = id;
			Model = model;
		}
	}

	public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, Unit>
	{
		private readonly IGenericRepository _countriesRepository;
		private readonly IMapper _mapper;

		public UpdateCountryCommandHandler(IGenericRepository countriesRepository, IMapper mapper)
		{
			_countriesRepository = countriesRepository;
			_mapper = mapper;
		}
		public async Task<Unit> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
		{
			var country = await _countriesRepository.GetOneAsync<Country>(request.Id);
			_mapper.Map(request.Model, country);
			await _countriesRepository.UpdateAsync<Country>(country);
			return Unit.Value;
		}
	}
}
