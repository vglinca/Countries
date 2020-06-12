using Countries.Api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;

namespace Countries.Api.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureControllers(this IServiceCollection services)
		{
			services.AddControllers(options =>
			{
				options.ReturnHttpNotAcceptable = true;
				options.CacheProfiles.Add(ApiConstants.CacheProfileName, new CacheProfile
				{
					Duration = 240
				});
				options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
				options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));
				options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
				options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status422UnprocessableEntity));
				options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
				options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
				options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status201Created));
				options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status204NoContent));
			})
				.AddNewtonsoftJson(action =>
				{
					action.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				})
				.AddXmlDataContractSerializerFormatters();
		}

		public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
		{
			services.AddHttpCacheHeaders(expirationOptions => {
				expirationOptions.MaxAge = 120;
				expirationOptions.CacheLocation = Marvin.Cache.Headers.CacheLocation.Public;
			}, 
			validOptions => {
				validOptions.MustRevalidate = true;
			});
		}

		public static void ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc(ApiConstants.SwaggerDocName, new OpenApiInfo
				{
					Title = ApiConstants.SwaggerDocTitle,
					Version = "1",
					Contact = new OpenApiContact
					{
						Email = "vitalii.glinka@yahoo.com",
						Name = "Vitaly GLinca",
					},
					License = new OpenApiLicense
					{
						Name = "MIT License",
						Url = new Uri("https://opensource.org/licenses/MIT")
					}
				});
			});
		}
	}
}
