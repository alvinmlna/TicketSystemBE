﻿using API.Common.Response;
using System.Data;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate next;
		private readonly ILogger<ExceptionMiddleware> logger;
		private readonly IHostEnvironment env;

		public ExceptionMiddleware(
			RequestDelegate next,
			ILogger<ExceptionMiddleware> logger,
			IHostEnvironment env)
		{
			this.next = next;
			this.logger = logger;
			this.env = env;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await next(context);
			}
            catch (DBConcurrencyException ex)
            {
                ExceptionThrow(context, HttpStatusCode.Conflict, ex);
            }
            catch (Exception ex)
			{
				ExceptionThrow(context, HttpStatusCode.InternalServerError, ex);
			}
		}

		private async void ExceptionThrow(HttpContext context, HttpStatusCode statusCode, Exception ex)
		{
            logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment()
                //DEV
                ? new ApiResponse(HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
                //PROD
                new ApiResponse(HttpStatusCode.InternalServerError);

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
	}
}
