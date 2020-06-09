using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Domain.Entities
{
	public class CountryLanguage : BaseEntity
	{
		public long CountryId { get; set; }
		public virtual Country Country { get; set; }
		public long LanguageId { get; set; }
		public virtual Language Language { get; set; }
	}
}
