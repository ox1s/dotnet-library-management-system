using Microsoft.OpenApi.Models;

namespace Library.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services
    )
    {
        services.AddAuthorization();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config =>
            config.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Система управления библиотекой",
                Description = "API для управления библиотекой",
                Version = "1.0"
            }));

        return services;
    }

    public static WebApplication UseApiServices(
        this WebApplication app
    )
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}
