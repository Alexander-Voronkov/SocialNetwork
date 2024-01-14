using AuthApi.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Net;

namespace AuthApi
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, logger) =>
            {
                var protocol = Environment.GetEnvironmentVariable("PROTOCOL") ?? "http";
                var serverUrl = Environment.GetEnvironmentVariable("SEQ_CONTAINER_NAME") ?? "localhost";
                logger.ReadFrom.Configuration(context.Configuration);
                logger.WriteTo.Seq($"{protocol}://{serverUrl}:5341");
            });

            builder.Services.AddRazorPages();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDbConnections();
            
            builder.Services.AddHealthChecks();

            builder.Services.AddCors();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            var protocol = Environment.GetEnvironmentVariable("PROTOCOL") ?? "http";

            if (protocol == "http")
            {
                app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
            }

            if (protocol == "https")
            {
                app.UseHttpsRedirection();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });

            app.UseSerilogRequestLogging();

            app.UseHealthChecks("/health");

            app.InitIdentityServerConfiguration().GetAwaiter().GetResult();

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