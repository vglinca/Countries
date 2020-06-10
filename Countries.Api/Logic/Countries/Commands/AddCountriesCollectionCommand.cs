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
		private readonly IGenericRepository _repository;
		private readonly IMapper _mapper;

		public AddCountriesCollectionCommandHandler(IGenericRepository countriesRepository, IMapper mapper)
		{
			_repository = countriesRepository;
			_mapper = mapper;
		}
		public async Task<Unit> Handle(AddCountriesCollectionCommand request, CancellationToken cancellationToken)
		{
			foreach (var model in request.Models)
			{
				var currency = _mapper.Map<Currency>(model.Currency);
				if (model.Currency.Id == null)
				{
					_ = await _repository.CreateAsync(currency);
				}
				var country = _mapper.Map<Country>(model);
				country.CurrencyId = currency.Id;
				var createdEntity = await _repository.CreateAsync(country);

				var langIds = model.Languages
					.Where(l => l.Id != 0)
					.Select(l => l.Id)
					.ToList();

				var languagesToCreate = model.Languages.Where(l => l.Id == 0);
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
			}
			return Unit.Value;
		}
	}
}
