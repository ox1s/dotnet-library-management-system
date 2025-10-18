using Library.DAL;
using Library.BLL;
using Library.API;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApiServices()
        .AddDataAccessLayer(builder.Configuration)
        .AddBusinessLogicLayer();
}


var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseApiServices();
    }

    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}