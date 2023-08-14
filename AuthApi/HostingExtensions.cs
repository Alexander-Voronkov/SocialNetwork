using AuthApi.Data;
using AuthApi.Models;
using Duende.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuthApi
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();

            var dbHost = Environment.GetEnvironmentVariable("AUTHDB_HOST");
            var dbPort = Environment.GetEnvironmentVariable("AUTHDB_PORT");
            var dbName = Environment.GetEnvironmentVariable("AUTHDB_NAME");
            var dbUser = Environment.GetEnvironmentVariable("AUTHDB_USERID");
            var dbPassword = Environment.GetEnvironmentVariable("AUTHDB_PASS");

            string connStr;

            if(dbHost != null)
            {
                connStr = $"Server={dbHost},{dbPort};Database={dbName};User Id={dbUser};Password={dbPassword}";
            }
            else
                connStr = "Server=127.0.0.1,1433;Database=USERCREDENTIALS;User Id=sa;Password=Admin_1234";
            
            var assembly = Assembly.GetExecutingAssembly().GetName().Name;


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connStr, options =>
                {
                    options.MigrationsAssembly(assembly);
                }));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
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