using Library.DAL.DataContext;

namespace Library.API.Seeder;

public static class DataSeeder
{
    public static async Task SeedDataAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            try
            {
                var dbContext = serviceProvider.GetRequiredService<LibraryDbContext>();

                var seeder = new FakerInitializer(dbContext);
                await seeder.InitializeAsync();
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>(); 
                logger.LogError(ex, "Произошла ошибка при начальном заполнении базы данных.");
            }
        }
    }
}