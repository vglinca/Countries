using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Domain.Entities
{
	public class Language : BaseEntity
	{
		public string Iso639_1 { get; set; }
		public string Iso639_2 { get; set; }
		public string Name { get; set; }
		public virtual ICollection<CountryLanguage> CountryLanguages { get; set; }
	}
}
