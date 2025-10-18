
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Library.BLL.DataContext;
using Library.Core.Interfaces;
using Library.DAL.Repositories;

namespace Library.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddSingleton<Core.Interfaces.IAuthorRepository, Repositories.InMemoryAuthorRepository>();
        // services.AddSingleton<Core.Interfaces.IBookRepository, Repositories.InMemoryBookRepository>();
        // ...- . .-.. .- ..--  .... --- .-. --- ---- . --. ---  -. .- ... - .-. --- . -. .. .-.-  -. .-  --- ... - .- .-- ---- .. .--- ... .-.-  -.. . -. -..- -.--.-

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}