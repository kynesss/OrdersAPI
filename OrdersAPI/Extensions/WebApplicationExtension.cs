using OrdersAPI.Middlewares;
using OrdersAPI.Seeders;

namespace OrdersAPI.Extensions
{
    public static class WebApplicationExtension
    {
        public static async Task SeedIdentity(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var identitySeeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
            await identitySeeder.Seed();
        }

        public static void UseMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}