using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Domain.Entities
{
	public class Region : BaseEntity
	{
		public string Name { get; set; }
		public double Area { get; set; }
		public virtual ICollection<Country> Countries { get; set; }
	}
}
