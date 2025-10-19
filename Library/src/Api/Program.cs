using Library.DAL;
using Library.BLL;
using Library.API;
using Library.API.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApiServices()
        .AddDataAccessLayer(builder.Configuration)
        .AddBusinessLogicLayer();
}


var app = builder.Build();
{
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseApiServices();
    }

    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}