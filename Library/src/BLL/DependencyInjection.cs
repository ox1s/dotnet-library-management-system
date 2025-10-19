using Microsoft.Extensions.DependencyInjection;
using Library.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Library.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddScoped<IAuthorService, Services.AuthorService>();
        services.AddScoped<IBookService, Services.BookService>();

        return services;
    }
}
