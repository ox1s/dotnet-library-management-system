
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddSingleton<Core.Interfaces.IAuthorRepository, Repositories.InMemoryAuthorRepository>();
        services.AddSingleton<Core.Interfaces.IBookRepository, Repositories.InMemoryBookRepository>();

        // services.AddDbContext<MyDbContext>(options => ...);

        return services;
    }
}