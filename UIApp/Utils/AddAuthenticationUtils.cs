using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddAuthenticationUtils(this IServiceCollection services)
        {
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
                        await client.GetAsync($"https://{(Environment.GetEnvironmentVariable("WEB_API_HOST") ?? "localhost") }:{(Environment.GetEnvironmentVariable("WebApiPort") ?? "7129")}/authenticate");
                    };
                    options.Events.OnSigningOut = async e =>
                    {
                        await e.HttpContext.RevokeUserRefreshTokenAsync();
                    };
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, config =>
                {
                    config.Authority = $"https://{(Environment.GetEnvironmentVariable("AUTH_API_HOST") ?? "localhost") + ":" + (Environment.GetEnvironmentVariable("AuthApiPort") ?? "7006")}";
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
                    config.RequireHttpsMetadata = true;
                    config.UseTokenLifetime = true;
                });

            services.AddAccessTokenManagement();

            return services;
        }
    }
}
