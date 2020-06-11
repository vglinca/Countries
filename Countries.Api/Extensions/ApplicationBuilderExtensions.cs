using Countries.Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Countries.Api.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		public static void UseExeptionHandling(this IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler(cfg => cfg.Run(async ctx => {
					ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					//var mag = ctx.Features.Get<IExceptionHandlerFeature>().Error.Message;
					await ctx.Response.WriteAsync("An unexpected fault happened. Try again later.");
				}));
			}
		}

		public static void UseExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionHandlingMiddleware>();
		}
	}
}
