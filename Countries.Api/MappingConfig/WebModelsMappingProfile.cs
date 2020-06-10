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
			CreateMap<LanguageCreateModel, Language>()
				.ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<LanguageUpdateModel, Language>();

			CreateMap<Country, CountryModel>()
				.ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region.Name))
				.ForMember(dest => dest.Languages, opt => opt.MapFrom(
					src => src.CountryLanguages.Select(cl => cl.Language)));
			CreateMap<CountryForCreationModel, Country>()
				.ForMember(dest => dest.CountryLanguages, opt => opt.Ignore())
				.ForMember(dest => dest.Currency, opt => opt.Ignore());
			CreateMap<CountryForUpdateModel, Country>();

			CreateMap<Region, RegionModel>();
		}
	}
}
