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
            var dbHost = Environment.GetEnvironmentVariable("AUTHDB_HOST") ?? "localhost";
            var dbPort = Environment.GetEnvironmentVariable("AUTHDB_PORT") ?? "1411";
            var dbName = Environment.GetEnvironmentVariable("AUTHDB_NAME") ?? "IDENTITYSERVERCONFIG";
            var dbUser = Environment.GetEnvironmentVariable("AUTHDB_USERID") ?? "sa";
            var dbPassword = Environment.GetEnvironmentVariable("AUTHDB_PASS") ?? "Admin_1234";
            string connStr = $"Server={dbHost},{dbPort};Database={dbName};User Id={dbUser};Password={dbPassword}";


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
