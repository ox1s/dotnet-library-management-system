
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddSingleton<Core.Interfaces.IAuthorsRepository, Repositories.InMemoryAuthorsRepository>();
        services.AddSingleton<Core.Interfaces.IBooksRepository, Repositories.InMemoryBooksRepository>();

        // services.AddDbContext<MyDbContext>(options => ...);

        return services;
    }
}