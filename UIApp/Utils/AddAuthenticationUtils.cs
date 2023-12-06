using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.Connections;

namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddAuthenticationUtils(this IServiceCollection services)
        {
            var protocol = Environment.GetEnvironmentVariable("PROTOCOL") ?? "http";
            var nginxprotocol = Environment.GetEnvironmentVariable("NGINX_PROTOCOL") ?? "http";
            var nginxauthapihost = Environment.GetEnvironmentVariable("NGINX_AUTH_API_HOST") ?? "nginx_authapi";
            var nginxauthapiport = Environment.GetEnvironmentVariable("NGINX_AUTHAPI_INNER_PORT") ?? "8082";
            var webapinginxhost = Environment.GetEnvironmentVariable("NGINX_WEB_API_HOST") ?? "nginx_webapi";
            var nginxwebapiport = Environment.GetEnvironmentVariable("NGINX_WEBAPI_INNER_PORT") ?? "8081";

            services
                .AddAuthentication(config =>
                {
                    config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Events.OnSignedIn = async context =>
                    {
                        var token = context.Properties.Items[".Token.access_token"];
                        var client = new HttpClient();
                        client.SetBearerToken(token);
                        await client.GetAsync($"{nginxprotocol}://{webapinginxhost}:{nginxwebapiport}/authenticate");
                    };
                    options.Events.OnSigningOut = async e =>
                    {
                        await e.HttpContext.RevokeUserRefreshTokenAsync();
                    };
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, config =>
                {
                    config.Authority = $"{nginxprotocol}://{nginxauthapihost}:{nginxauthapiport}";
                    config.ClientId = "WebUI";
                    config.ClientSecret = "WebUISecretToken";
                    config.SaveTokens = true;
                    config.ResponseType = "code";
                    config.Scope.Add("DataApi:read");
                    config.Scope.Add("phone");
                    config.Scope.Add("email");
                    config.Scope.Add("openid");
                    config.Scope.Add("profile");
                    config.Scope.Add("offline_access");
                    config.UseTokenLifetime = true;
                    if (nginxprotocol == "https")
                    {
                        config.RequireHttpsMetadata = true;
                    }
                    else
                    {
                        config.RequireHttpsMetadata = false;
                    }
                });

            services.AddAccessTokenManagement();

            return services;
        }
    }
}
