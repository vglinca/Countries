using AutoMapper;
using Countries.Api.Models.Countries;
using Countries.Api.Models.Currencies;
using Countries.Api.Models.Languages;
using Countries.Api.Models.Regions;
using Countries.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries.Api.MappingConfig
{
	public class WebModelsMappingProfile : Profile
	{
		public WebModelsMappingProfile()
		{
			CreateMap<Currency, CurrencyModel>();
			CreateMap<CurrencyCreateModel, Currency>();
			CreateMap<CurrencyUpdateModel, Currency>();

			CreateMap<Language, LanguageModel>();
			CreateMap<LanguageCreateModel, Language>();
			CreateMap<LanguageUpdateModel, Language>();

			CreateMap<Country, CountryModel>()
				.ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region.Name))
				.ForMember(dest => dest.Languages, opt => opt.MapFrom(
					src => src.CountryLanguages.Select(cl => cl.Language)));
			CreateMap<CountryForCreationModel, Country>();
			CreateMap<CountryForUpdateModel, Country>();

			CreateMap<Region, RegionModel>();
		}
	}
}
