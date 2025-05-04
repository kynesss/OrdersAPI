using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Entities;
using OrdersAPI.Middlewares;
using OrdersAPI.Seeders;
using OrdersAPI.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OrdersAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
        }

        public static void AddMiddlewares(this IServiceCollection services)
        {
            services.AddScoped<ErrorHandlingMiddleware>();
        }

        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new AuthenticationSettings();
            configuration.GetSection("Authentication").Bind(settings);
            services.AddSingleton(settings);

            services.AddAuthentication(builder =>
            {
                builder.DefaultScheme = "Bearer";
                builder.DefaultAuthenticateScheme = "Bearer";
                builder.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(builder =>
            {
                builder.RequireHttpsMetadata = false;
                builder.SaveToken = true;
                builder.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = settings.JwtIssuer,
                    ValidAudience = settings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtKey))
                };
            });
        }

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
    }
}