using Countries.Api.Models.Currencies;
using Countries.Api.Models.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries.Api.Models.Countries
{
	public class CountryForUpdateModel
	{
		public string Name { get; set; }
		public string Capital { get; set; }
		public double Area { get; set; }
		public int NumericCode { get; set; }
		public int Population { get; set; }
		public long RegionId { get; set; }
		public string SubRegion { get; set; }
		public CurrencyCreateModel Currency { get; set; }
		public List<LanguageCreateModel> Languages { get; set; }
		public string Alpha2Code { get; set; }
		public string Alpha3Code { get; set; }
	}
}
