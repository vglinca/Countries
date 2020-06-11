using Countries.Api.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries.Api.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc(ApiConstants.SwaggerDocName, new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Title = ApiConstants.SwaggerDocTitle,
					Version = "1",
					License = new Microsoft.OpenApi.Models.OpenApiLicense
					{
						Name = "MIT License"
					}
				});
			});
		}
	}
}
