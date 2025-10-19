
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Library.DAL.DataContext;
using Library.Core.Interfaces;
using Library.DAL.Repositories;

namespace Library.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // Для списков
        // ...- . .-.. .- ..--  .... --- .-. --- ---- . --. ---  -. .- ... - .-. --- . -. .. .-.-  -. .-  --- ... - .- .-- ---- .. .--- ... .-.-  -.. . -. -..- -.--.-
        // services.AddSingleton<IUnitOfWork, InMemoryUnitOfWork>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}