using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Countries.Api.Models.Regions;
using Countries.Core.Repository.Interfaces;
using Countries.Domain.Entities;
using MediatR;

namespace Countries.Api.Logic.Regions.Queries
{
	public class GetRegionsQuery : IRequest<IEnumerable<RegionModel>>
	{
	}

	public class GetRegionsQueryHandler : IRequestHandler<GetRegionsQuery, IEnumerable<RegionModel>>
	{
		private readonly IGenericRepository _regionsRepository;
		private readonly IMapper _mapper;
		public GetRegionsQueryHandler(IGenericRepository regionRepository, IMapper mapper)
		{
			_regionsRepository = regionRepository;
			_mapper = mapper;
		}
		public async Task<IEnumerable<RegionModel>> Handle(GetRegionsQuery request, CancellationToken cancellationToken)
		{
			var regions = await _regionsRepository.GetAllAsync<Region>();
			var models = _mapper.Map<IEnumerable<RegionModel>>(regions);
			return models;
		}
	}
}
