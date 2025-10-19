using System.Net;
using Library.BLL.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Library.API.ExceptionHandlers;

internal sealed class DuplicateExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<DuplicateExceptionHandler> logger) : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(
         HttpContext httpContext,
         Exception exception,
         CancellationToken cancellationToken)
    {
        if (exception is not (DuplicateAuthorException or DuplicateBookException))
        {
            return false;
        }

        logger.LogWarning(exception, $"Попытка является дубликатом: {exception.Message}");

        httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;

        await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Type = exception.GetType().Name,
                Title = "Обнаружен дубликат",
                Detail = exception.Message,
                Status = StatusCodes.Status409Conflict,
            }
        });

        return true;
    }
}
