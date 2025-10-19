using Library.DAL;
using Library.DAL.Seeder;
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
        .AddExceptionHandler<ValidationExceptionHandler>()
        .AddExceptionHandler<GlobalExceptionHandler>()
        .AddProblemDetails();
}


var app = builder.Build();
{
    await app.SeedDataAsync();
    app.UseExceptionHandler();

    if (app.Environment.IsDevelopment())
    {
        app.UseApiServices();
    }

    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}