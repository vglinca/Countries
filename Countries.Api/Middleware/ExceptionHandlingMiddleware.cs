using Countries.Api.Utils;
using Countries.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Countries.Api.Middleware
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _nxt;

		public ExceptionHandlingMiddleware(RequestDelegate nxt)
		{
			_nxt = nxt;
		}

		public async Task Invoke(HttpContext ctx)
		{
			try
			{
				await _nxt(ctx);
			}
			catch(NotFoundException e)
			{
				await HandleExeptionAsync(ctx, e, HttpStatusCode.NotFound);
			}
			catch (BadRequestException e)
			{
				await HandleExeptionAsync(ctx, e, HttpStatusCode.BadRequest);
			}
			catch (Exception e)
			{
				await HandleExeptionAsync(ctx, e, HttpStatusCode.InternalServerError);
			}
		}

		private async Task HandleExeptionAsync(HttpContext ctx, Exception e, HttpStatusCode statusCode)
		{
			var response = ctx.Response;
			response.ContentType = ApiConstants.ApplicationProblemJson;
			response.StatusCode = (int)statusCode;
			await response.WriteAsync(JsonConvert.SerializeObject(new
			{
				StatusCode = (int)statusCode,
				Error = e.Message
			}));
		}
	}
}
