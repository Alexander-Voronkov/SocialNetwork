using AuthApi.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace AuthApi
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDbConnections();
            
            builder.Services.AddHealthChecks();

            builder.Services.AddIdentityServer(x =>
            {
                var nginxprotocol = Environment.GetEnvironmentVariable("NGINX_PROTOCOL");
                var nginxauthapihost = Environment.GetEnvironmentVariable("NGINX_AUTH_API_HOST");
                var nginxauthapiport = Environment.GetEnvironmentVariable("NGINX_AUTHAPI_INNER_PORT");
                x.IssuerUri = $"{nginxprotocol}://{nginxauthapihost}:{nginxauthapiport}";
            });

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
          
            app.UseHealthChecks("/health");

            app.InitIdentityServerConfiguration().Wait();

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