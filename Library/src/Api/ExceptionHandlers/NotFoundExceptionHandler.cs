using System.Net;
using Library.BLL;
using Microsoft.AspNetCore.Diagnostics;

namespace Library.API.ExceptionHandlers;

internal sealed class NotFoundExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<NotFoundExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
         HttpContext httpContext,
         Exception exception,
         CancellationToken cancellationToken)
    {
        if (exception is not (AbsentAuthorException or AbsentBookException))
        {
            return false;
        }

        logger.LogWarning(exception, $"Запрошенный ресурс не найден: {exception.Message}");

        httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

        await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Type = exception.GetType().Name,
                Title = "Ресурс не найден",
                Detail = exception.Message,
                Status = StatusCodes.Status404NotFound,
            }
        });

        return true;
    }
}