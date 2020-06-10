using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Countries.Api.Models.Countries;
using Countries.Api.Models.Links;
using Countries.Api.Utils;
using Countries.Core.Infrastructure;
using Countries.Core.Repository.Interfaces;
using Countries.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using NWebsec.AspNetCore.Core.Web;

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
		private readonly HostUri _url;

		public GetCountryQueryHandler(IGenericRepository countriesRepository, IMapper mapper, IOptions<HostUri> uriOptions)
		{
			_countriesRepository = countriesRepository;
			_mapper = mapper;
			_url = uriOptions.Value;
		}
		public async Task<CountryModel> Handle(GetCountryQuery request, CancellationToken cancellationToken)
		{
			var country = await _countriesRepository.GetOneAsync<Country>(request.Id);
			var model = _mapper.Map<CountryModel>(country);
			return model;
		}

		private IEnumerable<LinkModel> CreateLinksForCountry(long id)
		{
			var links = new List<LinkModel>();
			links.Add(new LinkModel { Href = $"{_url}/api/countries/{id}", Rel = "self", Method = Constants.GET });
			links.Add(new LinkModel { Href = $"{_url}/api/countries/{id}", Rel = "self", Method = Constants.GET });
			links.Add(new LinkModel { Href = $"{_url}/api/countries/{id}", Rel = "self", Method = Constants.GET });
			return links;
		}
	}
}
