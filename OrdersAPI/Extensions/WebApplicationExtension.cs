using AutoMapper.Configuration.Annotations;
using OrdersAPI.Middlewares;
using OrdersAPI.Seeders;

namespace OrdersAPI.Extensions
{
    public static class WebApplicationExtension
    {
        public static async Task UseSeeders(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<ISeeder>();

            foreach (var seeder in seeders)
            {
                await seeder.Seed();
            }
        }

        public static void UseMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}