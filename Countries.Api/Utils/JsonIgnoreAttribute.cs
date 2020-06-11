using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Countries.Api.Utils
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class JsonIgnoreAttribute : JsonAttribute
	{
		public JsonIgnoreState IgnoreState { get; set; } = JsonIgnoreState.Always;
		public JsonIgnoreAttribute()
		{
		}
	}
}
