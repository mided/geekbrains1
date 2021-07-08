using Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Planner
{
    internal static class ErrorHandlerExtension
    {
        public static IApplicationBuilder UseDomainExceptions(this IApplicationBuilder builder)
        {
            builder.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;

                if (exception is NotFoundException)
                {
                    context.Response.StatusCode = 404;
                }

                var response = new { error = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            return builder;
        }
    }
}
