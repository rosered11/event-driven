using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;
using Rosered11.Common.Application;
using Rosered11.OrderService.Exception;

namespace Rosered11.OrderService.Application
{
    public static class OrderGlobalExceptionHandler
    {
        public static void UseCustomExceptionHandler(this WebApplication app) 
        { 
            app.UseExceptionHandler(exceptionHandler => {
                exceptionHandler.Run(async context => {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();
                    var logger = app.Services.GetRequiredService<ILogger<WebApplication>>();
                    
                    await ConfigOrderExceptionHandler(exceptionHandlerPathFeature, context, logger);
                    await GlobalExceptionHandler.ConfigExceptionHandler(exceptionHandlerPathFeature?.Error, context, logger);
                });
            }); 
        }

        private static async Task ConfigOrderExceptionHandler(IExceptionHandlerPathFeature? exceptionHandlerPathFeature, HttpContext context, ILogger logger)
        {
            if (exceptionHandlerPathFeature?.Error is OrderDomainException)
            {
                OrderDomainException? exception = (OrderDomainException?)exceptionHandlerPathFeature?.Error;
                logger.LogError(exception, exception?.Message);
                await context.Response.WriteAsync(new ErrorDTO(nameof(HttpStatusCode.BadRequest), exception?.Message).ToString());
            }

            if (exceptionHandlerPathFeature?.Error is OrderNotFoundException)
            {
                OrderNotFoundException? exception = (OrderNotFoundException?)exceptionHandlerPathFeature?.Error;
                logger.LogError(exception, exception?.Message);
                await context.Response.WriteAsync(new ErrorDTO(nameof(HttpStatusCode.NotFound), exception?.Message).ToString());
            }
        }
    }
}