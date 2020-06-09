using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries.Api.Models.Countries
{
	public class CountryForCreationModel
	{
		public string Name { get; set; }
		public string Capital { get; set; }
		public double Area { get; set; }
		public int NumericCode { get; set; }
		public int Population { get; set; }
		public long ContinentId { get; set; }
		public string Currency { get; set; }
		public string Alpha2Code { get; set; }
		public string Alpha3Code { get; set; }
		public string SubRegion { get; set; }
	}
}
