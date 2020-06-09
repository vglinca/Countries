using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Domain.Entities
{
	public class Currency : BaseEntity
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public virtual ICollection<Country> Countries { get; set; }
	}
}
