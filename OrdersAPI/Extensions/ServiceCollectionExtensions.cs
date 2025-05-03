using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Entities;
using OrdersAPI.Seeders;

namespace OrdersAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<IdentitySeeder>();
        }

        public static void AddServices(this IServiceCollection services)
        {
        }
    }
}