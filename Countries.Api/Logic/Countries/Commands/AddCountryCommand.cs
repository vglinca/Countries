using System.Collections.Generic;
using System.Linq;
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
		private readonly IGenericRepository _repository;
		private readonly IMapper _mapper;

		public AddCountryCommandHandler(IGenericRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public async Task<long> Handle(AddCountryCommand request, CancellationToken cancellationToken)
		{
			var currency = _mapper.Map<Currency>(request.Model.Currency);
			if (request.Model.Currency.Id == null)
			{
				await _repository.CreateAsync(currency);
			}
			var country = _mapper.Map<Country>(request.Model);
			country.CurrencyId = currency.Id;
			var createdEntity = await _repository.CreateAsync(country);

			var langIds = request.Model.Languages
				.Where(l => l.Id != 0)
				.Select(l => l.Id)
				.ToList();

			var languagesToCreate = request.Model.Languages.Where(l => l.Id == 0);
			if (languagesToCreate.Any())
			{
				var languageEntities = _mapper.Map<IEnumerable<Language>>(languagesToCreate);
				var ids = await _repository.CreateAsync(languageEntities);
				langIds.AddRange(ids);
			}

			foreach (var langId in langIds)
			{
				var countryLanguage = new CountryLanguage { CountryId = createdEntity.Id, LanguageId = langId };
				await _repository.CreateAsync(countryLanguage);
			}

			return createdEntity.Id;
		}
	}
}
