using Library.DAL;
using Library.BLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(config =>
    config.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Система управления библиотекой",
        Description = "API для управления библиотекой",
        Version = "1.0"
    }));

    builder.Services
        .AddDataAccessLayer()
        .AddBusinessLogicLayer();
}


var app = builder.Build();
{
    //app.UseExceptionHandler();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
