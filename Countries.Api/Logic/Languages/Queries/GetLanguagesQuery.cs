using AutoMapper;
using Countries.Api.Models.Languages;
using Countries.Core.Infrastructure;
using Countries.Core.Repository.Interfaces;
using Countries.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Countries.Api.Logic.Languages.Queries
{
	public class GetLanguagesQuery : IRequest<IEnumerable<LanguageModel>>
	{
		public PageArguments PageArgs { get; }
		public SortingArguments SortingArgs { get; }
		public FilterArguments FilterArgs { get; set; }

		public GetLanguagesQuery(PageArguments pageArgs, SortingArguments sortingArgs, FilterArguments filterArgs)
		{
			PageArgs = pageArgs;
			SortingArgs = sortingArgs;
			FilterArgs = filterArgs;
		}
	}

	public class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, IEnumerable<LanguageModel>>
	{
		private readonly IGenericRepository _repository;
		private readonly IMapper _mapper;

		public GetLanguagesQueryHandler(IGenericRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public async Task<IEnumerable<LanguageModel>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
		{
			var filterArgs = new List<FilterArguments> { request.FilterArgs };
			var pagedLanguages = await _repository.GetAllAsync<Language>(request.PageArgs, request.SortingArgs, filterArgs, LogicalOperator.Or);
			var models = _mapper.Map<IEnumerable<LanguageModel>>(pagedLanguages.Items);
			return models;
		}
	}
}
