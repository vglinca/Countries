using Countries.Api.Models.Currencies;
using Countries.Api.Models.Languages;
using Countries.Api.Models.Links;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Countries.Api.Models.Countries
{
	public class CountryModel
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Capital { get; set; }
		public double Area { get; set; }
		public string NumericCode { get; set; }
		public int Population { get; set; }
		public string Region { get; set; }
		public string SubRegion { get; set; }
		public CurrencyModel Currency { get; set; }
		public List<LanguageModel> Languages { get; set; }
		public string Alpha2Code { get; set; }
		public string Alpha3Code { get; set; }
		public IEnumerable<LinkModel> Links { get; set; }
	}
}
