using Trader.Backend.Api.AppContext;

namespace Trader.Backend.Api.Extentions
{
    public static class DatabaseExtentions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<TraderAppContext>();

            context.Database.MigrateAsync().GetAwaiter().GetResult();
        }
    }
}
