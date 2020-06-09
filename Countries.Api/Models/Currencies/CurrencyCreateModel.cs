using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries.Api.Models.Currencies
{
	public class CurrencyCreateModel
	{
		public string Code { get; set; }
		public string Name { get; set; }
	}
}
