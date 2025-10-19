using Library.DAL;
using Library.BLL;
using Library.API;
using Library.API.ExceptionHandlers;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApiServices()
        .AddDataAccessLayer(builder.Configuration)
        .AddBusinessLogicLayer()
        .AddExceptionHandler<NotFoundExceptionHandler>()
        .AddExceptionHandler<DuplicateExceptionHandler>()
        .AddExceptionHandler<GlobalExceptionHandler>()
        .AddProblemDetails();
}


var app = builder.Build();
{
    app.UseExceptionHandler();

    if (app.Environment.IsDevelopment())
    {
        app.UseApiServices();
    }

    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}