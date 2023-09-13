using AuthApi.Data;
using AuthApi.Models;
using Duende.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.Reflection;

namespace AuthApi
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();

            var dbHost = Environment.GetEnvironmentVariable("AUTHDB_HOST") ?? "127.0.0.1";
            var dbPort = Environment.GetEnvironmentVariable("AUTHDB_PORT") ?? "1411";
            var dbName = Environment.GetEnvironmentVariable("AUTHDB_NAME") ?? "IDENTITYSERVERCONFIG";
            var dbUser = Environment.GetEnvironmentVariable("AUTHDB_USERID") ?? "sa";
            var dbPassword = Environment.GetEnvironmentVariable("AUTHDB_PASS") ?? "Admin_1234";
            var identityDbHost = Environment.GetEnvironmentVariable("IDENTITY_HOST") ?? "127.0.0.1";
            var identityDbPort = Environment.GetEnvironmentVariable("IDENTITY_PORT") ?? "1422";
            var identityDbName = Environment.GetEnvironmentVariable("IDENTITY_NAME") ?? "USERS_IDENTITY_DB";
            var identityDbUser = Environment.GetEnvironmentVariable("IDENTITY_USERID") ?? "sa";
            var identityDbPassword = Environment.GetEnvironmentVariable("IDENTITY_PASS") ?? "Admin_1234";

            string connStr = $"Server={dbHost},{dbPort};Database={dbName};User Id={dbUser};Password={dbPassword}";
            string identityConnStr = $"Server={dbHost},{dbPort};Database={dbName};User Id={dbUser};Password={dbPassword}";
            
            var assembly = Assembly.GetExecutingAssembly().GetName().Name;

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(identityConnStr, options =>
                {
                    options.MigrationsAssembly(assembly);
                }));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services
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

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.InitIdentityServerConfiguration();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.MapRazorPages()
                .RequireAuthorization();

            return app;
        }
    }
}