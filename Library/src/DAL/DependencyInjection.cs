
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Library.BLL.DataContext;

namespace Library.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddSingleton<Core.Interfaces.IAuthorRepository, Repositories.InMemoryAuthorRepository>();
        services.AddSingleton<Core.Interfaces.IBookRepository, Repositories.InMemoryBookRepository>();
        //services.AddDbContext<MSSqlAuthorDbContext>(options =>
        //  options.UseSqlServer("Data Source = "));
        // services.AddDbContext<MyDbContext>(options => ...);

        return services;
    }
}