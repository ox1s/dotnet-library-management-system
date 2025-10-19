using System.Net;
using System.Text.Json;
using Library.BLL;

namespace Library.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла необработанная ошибка.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError; 
        var responseMessage = "Произошла внутренняя ошибка сервера.";

        switch (exception)
        {
            case AbsentAuthorException or AbsentBookException:
                statusCode = HttpStatusCode.NotFound; 
                responseMessage = exception.Message;
                break;
            case DuplicateAuthorException or DuplicateBookException:
                statusCode = HttpStatusCode.Conflict; 
                responseMessage = exception.Message;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var result = JsonSerializer.Serialize(new { error = responseMessage });
        await context.Response.WriteAsync(result);
    }
}
