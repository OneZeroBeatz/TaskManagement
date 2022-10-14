using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using TaskManagement.Api.Errors;

namespace TaskManagement.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        var errorDetails = CreateErrorDetails(context);
                        context.Response.StatusCode = errorDetails.StatusCode;
                        await context.Response.WriteAsync(errorDetails.ToString());
                    }

                });
            });
        }

        private static ErrorDetails CreateErrorDetails(HttpContext context)
        {
            return new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal server error"
            };
        }
    }
}
