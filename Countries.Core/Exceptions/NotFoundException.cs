using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.Core.Exceptions
{
	public sealed class NotFoundException : Exception
	{
		public NotFoundException()
		{
		}
		public NotFoundException(string msg) : base(msg)
		{
		}
	}
}
