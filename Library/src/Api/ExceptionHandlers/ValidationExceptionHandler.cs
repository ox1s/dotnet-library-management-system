using System.Net;
using Library.BLL;
using Microsoft.AspNetCore.Diagnostics;

namespace Library.API.ExceptionHandlers;

internal sealed class ValidationExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<ValidationExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
         HttpContext httpContext,
         Exception exception,
         CancellationToken cancellationToken)
    {
        if (exception is not ImpossibleDateException)
        {
            return false;
        }

        logger.LogWarning(exception, $"Введенная дата не корректна: {exception.Message}");

        httpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;

        await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Type = exception.GetType().Name,
                Title = "Дата не действительна",
                Detail = exception.Message,
                Status = StatusCodes.Status422UnprocessableEntity,
            }
        });

        return true;
    }
}