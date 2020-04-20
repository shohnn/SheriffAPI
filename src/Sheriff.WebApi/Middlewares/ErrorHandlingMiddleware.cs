using System;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

namespace Sheriff.WebApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration configuration;

        public ErrorHandlingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            this.configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (context.Response.HasStarted)
                throw exception;

            var response = new ErrorResponse
            {
                Error = exception.Message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;

            return JsonSerializer.SerializeAsync(context.Response.Body, response);
        }
    }
}