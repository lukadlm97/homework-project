using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using Homework.Enigmatry.Application.Shared.DTOs.Error;
using Homework.Enigmatry.Application.Shared.Exceptions;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Microsoft.Extensions.Logging;
using ValidationException = FluentValidation.ValidationException;
using System.Net.Http;
using Homework.Enigmatry.Shop.Application.Exceptions;

namespace Homework.Enigmatry.Shop.Presentation.Middlewares
{
    public class ExceptionMiddleware:IMiddleware
    {
        private readonly IHighPerformanceLogger _highPerformanceLogger;
        private readonly LogTraceData _logTraceData;

        public ExceptionMiddleware(IHighPerformanceLogger highPerformanceLogger, LogTraceData logTraceData)
        {
            _highPerformanceLogger = highPerformanceLogger;
            _logTraceData = logTraceData;
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
                    _highPerformanceLogger.Log("Internal server error", unclearOperationsResultException, LogLevel.Error);
                    break;
                case UnavailableAtLocalPersistenceStorageException ex:
                    statusCode = HttpStatusCode.InternalServerError;
                    _highPerformanceLogger.Log("Internal server error:"+ex.Message, ex, LogLevel.Error);
                    break;
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    _highPerformanceLogger.Log("Bad request", validationException, LogLevel.Error);
                    break;
                default:
                    _highPerformanceLogger.Log("Unknown error", LogLevel.Error);
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            finally
            {
                _highPerformanceLogger.Log(string.Join("=>\n", _logTraceData.RequestPath), LogLevel.Information);
            }
        }
    }
}
