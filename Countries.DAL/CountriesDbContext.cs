using Countries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Countries.DAL
{
	public class CountriesDbContext : DbContext
	{
		public DbSet<Region> Regions { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Currency> Currencies { get; set; }
		public DbSet<Language> Languages { get; set; }
		public DbSet<CountryLanguage> CountryLanguages { get; set; }

		public CountriesDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies();
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			ApplyEntitiesConfiguration(modelBuilder);
			SeedData(modelBuilder);
		}

		
		private void ApplyEntitiesConfiguration(ModelBuilder modelBuilder)
		{
			var typesToRegister = Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(t => !string.IsNullOrWhiteSpace(t.Namespace))
				.Where(t => t.BaseType != null && t.BaseType.IsInterface &&
					t.BaseType.IsGenericType &&
					t.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));

			foreach (var type in typesToRegister)
			{
				dynamic config = Activator.CreateInstance(type);
				modelBuilder.ApplyConfiguration(config);
			}
		}

		private void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Region>().HasData(new List<Region> { 
				new Region{Id = 1, Name = "Europe", Area = 10180000},
				new Region{Id = 2, Name = "Asia", Area = 44580000},
				new Region{Id = 3, Name = "Australia and Oceania", Area = 8526000},
				new Region{Id = 4, Name = "Africa", Area = 30370000},
				new Region{Id = 5, Name = "Americas", Area = 42550000},
			});

			modelBuilder.Entity<Currency>().HasData(new List<Currency> { 
				new Currency{Id = 1, Code = "EUR", Name = "Euro"},
				new Currency{Id = 2, Code = "ALL", Name = "Albanian lek"},
				new Currency{Id = 3, Code = "CZK", Name = "Czech koruna"},
				new Currency{Id = 4, Code = "DKK", Name = "Danish krone"},
				new Currency{Id = 5, Code = "ISK", Name = "Icelandic króna"},
				new Currency{Id = 6, Code = "JPY", Name = "Japanese yen"},
				new Currency{Id = 7, Code = "JOD", Name = "Jordanian dinar"},
				new Currency{Id = 8, Code = "KWD", Name = "Kuwaiti dinar"},
				new Currency{Id = 9, Code = "MYR", Name = "Malaysian ringgit"},
				new Currency{Id = 10, Code = "CAD", Name = "Canadian dollar"},
				new Currency{Id = 11, Code = "ARS", Name = "Argentine peso"},
				new Currency{Id = 12, Code = "BOB", Name = "Bolivian boliviano"},
			});

			modelBuilder.Entity<Language>().HasData(new List<Language> {
				new Language{Id = 1, Iso639_1 = "et", Iso639_2 = "est", Name = "Estonian"},
				new Language{Id = 2, Iso639_1 = "sq", Iso639_2 = "sqi", Name = "Albanian"},
				new Language{Id = 3, Iso639_1 = "cs", Iso639_2 = "ces", Name = "Czech"},
				new Language{Id = 4, Iso639_1 = "sk", Iso639_2 = "slk", Name = "Slovak"},
				new Language{Id = 5, Iso639_1 = "fo", Iso639_2 = "fao", Name = "Faroese"},
				new Language{Id = 6, Iso639_1 = "de", Iso639_2 = "deu", Name = "German"},
				new Language{Id = 7, Iso639_1 = "is", Iso639_2 = "isl", Name = "Icelandic"},
				new Language{Id = 8, Iso639_1 = "ja", Iso639_2 = "jpn", Name = "Japanese"},
				new Language{Id = 9, Iso639_1 = "ar", Iso639_2 = "ara", Name = "Arabic"},
				new Language{Id = 10, Iso639_1 = "ar", Iso639_2 = "ara", Name = "Arabic"},
				new Language{Id = 11, Iso639_1 = "", Iso639_2 = "zsm", Name = "Malaysian"},
				new Language{Id = 12, Iso639_1 = "en", Iso639_2 = "eng", Name = "English"},
				new Language{Id = 13, Iso639_1 = "fr", Iso639_2 = "fra", Name = "French"},
				new Language{Id = 14, Iso639_1 = "es", Iso639_2 = "spa", Name = "Spanish"},
				new Language{Id = 15, Iso639_1 = "gn", Iso639_2 = "grn", Name = "Guarani"},
				new Language{Id = 16, Iso639_1 = "ay", Iso639_2 = "aym", Name = "Aymara"},
				new Language{Id = 17, Iso639_1 = "qu", Iso639_2 = "que", Name = "Quechua"},
			});

			modelBuilder.Entity<Country>().HasData(new List<Country> { 
				new Country
				{
					Id = 1, 
					Name = "Estonia", 
					Capital = "Tallinn", 
					Alpha2Code = "EE",
					Alpha3Code = "EST",
					RegionId = 1, 
					CurrencyId = 1, 
					NumericCode = 233, 
					Population = 1315944, 
					Area = 45227.0,
					SubRegion = "Northern Europe",
				},
				new Country
				{
					Id = 2,
					Name = "Albania",
					Capital = "Tirana",
					Alpha2Code = "AL",
					Alpha3Code = "ALB",
					RegionId = 1,
					CurrencyId = 2,
					NumericCode = 008,
					Population = 2886026,
					Area = 28748.0,
					SubRegion = "Southern Europe"
				},
				new Country
				{
					Id = 3,
					Name = "Czech Republic",
					Capital = "Prague",
					Alpha2Code = "CZ",
					Alpha3Code = "CZE",
					RegionId = 1,
					CurrencyId = 3,
					NumericCode = 203,
					Population = 10558524,
					Area = 78865.0,
					SubRegion = "Western Europe"
				},
				new Country
				{
					Id = 4,
					Name = "Faroe Islands",
					Capital = "Tórshavn",
					Alpha2Code = "DK",
					Alpha3Code = "DNK",
					RegionId = 1,
					CurrencyId = 4,
					NumericCode = 234,
					Population = 49376,
					Area = 1393.0,
					SubRegion = "Northern Europe"
				},
				new Country
				{
					Id = 5,
					Name = "Germany",
					Capital = "Berlin",
					Alpha2Code = "DE",
					Alpha3Code = "DEU",
					RegionId = 1,
					CurrencyId = 1,
					NumericCode = 276,
					Population = 81770900,
					Area = 357114.0,
					SubRegion = "Western Europe"
				},
				new Country
				{
					Id = 6,
					Name = "Iceland",
					Capital = "Reykjavík",
					Alpha2Code = "IS",
					Alpha3Code = "ISL",
					RegionId = 1,
					CurrencyId = 5,
					NumericCode = 352,
					Population = 334300,
					Area = 103000.0,
					SubRegion = "Northern Europe"
				},
				new Country
				{
					Id = 7,
					Name = "Japan",
					Capital = "Tokyo",
					Alpha2Code = "JP",
					Alpha3Code = "JPN",
					RegionId = 2,
					CurrencyId = 6,
					NumericCode = 392,
					Population = 126960000,
					Area = 377930.0,
					SubRegion = "Eastern Asia"
				},
				new Country
				{
					Id = 8,
					Name = "Jordan",
					Capital = "Amman",
					Alpha2Code = "JO",
					Alpha3Code = "JOR",
					RegionId = 2,
					CurrencyId = 7,
					NumericCode = 400,
					Population = 9531712,
					Area = 89342.0,
					SubRegion = "Western Asia"
				},
				new Country
				{
					Id = 9,
					Name = "Kuwait",
					Capital = "Kuwait City",
					Alpha2Code = "KW",
					Alpha3Code = "KWT",
					RegionId = 2,
					CurrencyId = 8,
					NumericCode = 414,
					Population = 4183658,
					Area = 17818.0,
					SubRegion = "Western Asia"
				},
				new Country
				{
					Id = 10,
					Name = "Malaysia",
					Capital = "Kuala Lumpur",
					Alpha2Code = "MY",
					Alpha3Code = "MYS",
					RegionId = 2,
					CurrencyId = 9,
					NumericCode = 458,
					Population = 31405416,
					Area = 45227.0,
					SubRegion = "South-Eastern Asia"
				},
				new Country
				{
					Id = 11,
					Name = "Canada",
					Capital = "Ottawa",
					Alpha2Code = "CA",
					Alpha3Code = "CAN",
					RegionId = 5,
					CurrencyId = 10,
					NumericCode = 124,
					Population = 36155487,
					Area = 9984670.0,
					SubRegion = "North America"
				},
				new Country
				{
					Id = 12,
					Name = "Argentina",
					Capital = "Buenos Aires",
					Alpha2Code = "AR",
					Alpha3Code = "ARG",
					RegionId = 5,
					CurrencyId = 11,
					NumericCode = 414,
					Population = 43590400,
					Area = 2780400.0,
					SubRegion = "South America"
				},
				new Country
				{
					Id = 13,
					Name = "Bolivia",
					Capital = "Sucre",
					Alpha2Code = "BO",
					Alpha3Code = "BOL",
					RegionId = 5,
					CurrencyId = 12,
					NumericCode = 068,
					Population = 10985059,
					Area = 1098581.0,
					SubRegion = "South America"
				}
			});

			modelBuilder.Entity<CountryLanguage>().HasData(new List<CountryLanguage> { 
				new CountryLanguage{Id = 1, CountryId = 1, LanguageId = 1},
				new CountryLanguage{Id = 2, CountryId = 2, LanguageId = 2},
				new CountryLanguage{Id = 3, CountryId = 3, LanguageId = 3},
				new CountryLanguage{Id = 4, CountryId = 3, LanguageId = 4},
				new CountryLanguage{Id = 5, CountryId = 4, LanguageId = 5},
				new CountryLanguage{Id = 6, CountryId = 5, LanguageId = 6},
				new CountryLanguage{Id = 7, CountryId = 6, LanguageId = 7},
				new CountryLanguage{Id = 8, CountryId = 7, LanguageId = 8},
				new CountryLanguage{Id = 9, CountryId = 8, LanguageId = 9},
				new CountryLanguage{Id = 10, CountryId = 9, LanguageId = 9},
				new CountryLanguage{Id = 11, CountryId = 10, LanguageId = 11},
				new CountryLanguage{Id = 12, CountryId = 11, LanguageId = 12},
				new CountryLanguage{Id = 13, CountryId = 11, LanguageId = 13},
				new CountryLanguage{Id = 14, CountryId = 12, LanguageId = 14},
				new CountryLanguage{Id = 15, CountryId = 12, LanguageId = 15},
				new CountryLanguage{Id = 16, CountryId = 13, LanguageId = 14},
				new CountryLanguage{Id = 17, CountryId = 13, LanguageId = 16},
				new CountryLanguage{Id = 18, CountryId = 13, LanguageId = 17},
			});
		}
	}
}
