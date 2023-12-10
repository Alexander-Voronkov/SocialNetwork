using AuthApi.Data;
using AuthApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuthApi.Utils
{
    public static class Utils
    {
        public static IServiceCollection AddDbConnections(this IServiceCollection services)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "1433";
            var dbName = Environment.GetEnvironmentVariable("AUTH_DB_NAME") ?? "IDENTITYSERVERCONFIG";
            var dbUser = Environment.GetEnvironmentVariable("DB_USERID") ?? "sa";
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASS") ?? "Admin_1234";
            string connStr = $"Data Source={dbHost},{dbPort};Initial Catalog={dbName};User ID={dbUser};Password={dbPassword};Encrypt=false";


            var assembly = Assembly.GetExecutingAssembly().GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connStr, options =>
                {
                    options.MigrationsAssembly(assembly);
                }));

            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddIdentityServer()
                .AddOperationalStore(config =>
                {
                    config.ConfigureDbContext = b =>
                    {
                        b.UseSqlServer(connStr, options =>
                        {
                            options.MigrationsAssembly(assembly);
                        });
                    };
                })
                .AddConfigurationStore(config =>
                {
                    config.ConfigureDbContext = b =>
                    {
                        b.UseSqlServer(connStr, options =>
                        {
                            options.MigrationsAssembly(assembly);
                        });
                    };
                })
                .AddAspNetIdentity<ApplicationUser>()
                .AddDeveloperSigningCredential();

            return services;
        }
    }
}
