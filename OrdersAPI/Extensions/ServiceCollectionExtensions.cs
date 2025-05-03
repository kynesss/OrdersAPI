using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Entities;
using OrdersAPI.Middlewares;
using OrdersAPI.Seeders;
using OrdersAPI.Services;

namespace OrdersAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
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

        public static void AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Program).Assembly)
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
        }

        public static void AddMiddlewares(this IServiceCollection services)
        {
            services.AddScoped<ErrorHandlingMiddleware>();
        }
    }
}