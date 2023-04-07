
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using Homework.Enigmatry.Application.Shared.DTOs.Error;
using Homework.Enigmatry.Application.Shared.Exceptions;

namespace Homework.Enigmatry.Shop.Presentation.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string result = JsonSerializer.Serialize(new ErrorDetailsDto(exception.Message, "Failure"));

            switch (exception)
            {
                case UnclearOperationsResultException unclearOperationsResultException:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
                default:
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
