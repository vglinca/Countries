using System;

namespace Countries.Core.Exceptions
{
	public sealed class BadRequestException : Exception
	{
		public BadRequestException()
		{
		}

		public BadRequestException(string msg) : base(msg)
		{
		}

		public BadRequestException(string msg, Exception ex) : base(msg, ex)
		{
		}
	}
}
