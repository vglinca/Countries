using AutoMapper;
using Countries.Api.Models.Countries;
using Countries.Core.Repository.Interfaces;
using Countries.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Countries.Api.Logic.Countries.Commands
{
	public class AddCountriesCollectionCommand : IRequest<Unit>
	{
		public List<CountryForCreationModel> Models { get; }

		public AddCountriesCollectionCommand(List<CountryForCreationModel> models)
		{
			Models = models;
		}
	}

	public class AddCountriesCollectionCommandHandler : IRequestHandler<AddCountriesCollectionCommand, Unit>
	{
		private readonly IGenericRepository _countriesRepository;
		private readonly IMapper _mapper;

		public AddCountriesCollectionCommandHandler(IGenericRepository countriesRepository, IMapper mapper)
		{
			_countriesRepository = countriesRepository;
			_mapper = mapper;
		}
		public async Task<Unit> Handle(AddCountriesCollectionCommand request, CancellationToken cancellationToken)
		{
			var countries = _mapper.Map<IEnumerable<Country>>(request.Models);
			_ = await _countriesRepository.CreateAsync(countries);
			return Unit.Value;
		}
	}
}
