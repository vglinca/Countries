using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Countries.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using AutoMapper;
using Countries.Core.Repository.Interfaces;
using Countries.Core.Repository;
using Countries.Api.MappingConfig;
using Countries.Api.Utils;

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
			
			services.AddControllers();

			var hostUriSection = Configuration.GetSection("HostUri");
			services.Configure<HostUri>(hostUriSection);

			services.AddAutoMapper(typeof(WebModelsMappingProfile).Assembly);
			services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
			services.AddHttpContextAccessor();

			services.AddScoped<IGenericRepository, GenericRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
