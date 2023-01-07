using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Rosered11.Common.Application
{
    public static class GlobalExceptionHandler
    {
        public static async Task ConfigExceptionHandler(Exception? exception, HttpContext context, ILogger logger)
        {
            if (exception is Exception && exception.GetType() == typeof(Exception))
            {
                logger.LogError(exception, exception?.Message);
                await context.Response.WriteAsync(new ErrorDTO(nameof(HttpStatusCode.InternalServerError), "Unexpected error!").ToString());
            }

            // if (exceptionHandlerPathFeature?.Error is OrderNotFoundException)
            // {
            //     OrderNotFoundException? exception = (OrderNotFoundException?)exceptionHandlerPathFeature?.Error;
            //     logger.LogError(exception, exception?.Message);
            //     await context.Response.WriteAsync(new ErrorDTO(nameof(HttpStatusCode.NotFound), exception?.Message).ToString());
            // }
        }
    }
}