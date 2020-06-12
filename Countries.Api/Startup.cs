using System;
using Countries.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using AutoMapper;
using Countries.Core.Repository.Interfaces;
using Countries.Core.Repository;
using Countries.Api.MappingConfig;
using Countries.Api.Utils;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Countries.Api.Utils.Interfaces;
using Countries.Api.Extensions;

namespace Countries.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = Configuration["ConnectionStrings:CountriesDbConnString"];
			services.AddDbContext<CountriesDbContext>(opt => opt.UseSqlServer(connectionString));

			services.ConfigureHttpCacheHeaders();
			services.AddResponseCaching();
			services.ConfigureControllers();

			var hostUriSection = Configuration.GetSection("HostUri");
			services.Configure<HostUri>(hostUriSection);

			services.AddAutoMapper(typeof(WebModelsMappingProfile).Assembly);
			services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
			services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
			services.AddHttpContextAccessor();

			services.AddScoped<IGenericRepository, GenericRepository>();
			services.AddTransient<ILinkProcessor, LinkProcessor>();

			services.ConfigureSwagger();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseExeptionHandling(env);
			app.UseExceptionMiddleware();
			app.UseResponseCaching();
			app.UseHttpCacheHeaders();
			app.UseRouting();
			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/CountriesOpenApi/swagger.json", ApiConstants.SwaggerDocTitle);
				options.RoutePrefix = "";
			});
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
