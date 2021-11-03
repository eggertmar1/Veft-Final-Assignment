using System.Net;
using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Models.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace JustTradeIt.Software.API.Middlewares
{
    public static class ExceptionHandler
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {

            app.UseExceptionHandler(error =>
                {
                    error.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature == null) return;

                        var exception = exceptionHandlerFeature.Error;
                        var message = exception.Message;
                        var statusCode = HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        if (exception is ResourceAlreadyExistsException)
                            statusCode = HttpStatusCode.Conflict;
                        else if (exception is ForbiddenException)
                            statusCode = HttpStatusCode.Forbidden;
                        else if (exception is InvalidRequestException)
                            statusCode = HttpStatusCode.BadRequest;
                        else if (exception is ResourceNotFoundException)
                            statusCode = HttpStatusCode.NotFound;
                        else 
                            message = "An error has occurred, please try again later";

                        context.Response.StatusCode = (int)statusCode;

                        await context.Response.WriteAsync(
                            new ExceptionModel
                            {
                                StatusCode = (int)statusCode,
                                ExceptionMessage = message
                            }.ToString()
                        );
                    });
                });
        }
    }
}

// resourcenotfound 
// forbidden exception
//resource already exists
//invalid requests
