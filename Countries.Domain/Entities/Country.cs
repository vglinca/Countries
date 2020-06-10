using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Domain.Entities
{
	public class Country : BaseEntity
	{
		public string Name { get; set; }
		public string Capital { get; set; }
		public double Area { get; set; }
		public string NumericCode { get; set; }
		public int Population { get; set; }
		public long RegionId { get; set; }
		public virtual Region Region { get; set; }
		public string SubRegion { get; set; }
		public long CurrencyId { get; set; }
		public virtual Currency Currency { get; set; }
		public string Alpha2Code { get; set; }
		public string Alpha3Code { get; set; }
		public virtual ICollection<CountryLanguage> CountryLanguages { get; set; }
	}
}
