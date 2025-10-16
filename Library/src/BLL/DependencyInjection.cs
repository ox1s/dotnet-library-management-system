using Microsoft.Extensions.DependencyInjection;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddScoped<IAuthorsService, Services.AuthorsService>();
        // services.AddScoped<IBookService, BookService>();

        return services;
    }
}
