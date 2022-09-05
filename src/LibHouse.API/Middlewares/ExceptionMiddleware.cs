using KissLog;
using LibHouse.API.Extensions.Common;
using LibHouse.Business.Notifiers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace LibHouse.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _nextRequest;

        public ExceptionMiddleware(RequestDelegate nextRequest)
        {
            _nextRequest = nextRequest;
        }

        public async Task InvokeAsync(HttpContext httpContext, IKLogger logger)
        {
            try
            {
                await _nextRequest(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, httpContext);

                logger.Log(LogLevel.Error, ex);
            }
        }

        private async static Task HandleExceptionAsync(
            Exception exception,
            HttpContext httpContext)
        {
            if (!httpContext.Response.HasStarted)
            {
                httpContext.Response.StatusCode = new StatusCodeResult(500).StatusCode;

                httpContext.Response.ContentType = MediaTypeNames.Application.Json;

                Notification exceptionResponse = new(exception.Message, $"Unexpected error on {httpContext.Request.Path.Value}");

                await httpContext.Response.WriteAsync(exceptionResponse.ToJson());
            }
        }
    }
}