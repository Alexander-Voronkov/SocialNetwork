using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Diagnostics.Contracts;

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
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, config =>
                {
                    config.Authority = "https://localhost:7006";
                    config.ClientId = "WebUI";
                    config.ClientSecret = "WebUISecretToken";
                    config.SaveTokens = true;
                    config.ResponseType = "code";
                    config.Scope.Add("DataApi:read");
                    config.Scope.Add("phone");
                    config.Scope.Add("email");
                    config.Scope.Add("openid");
                    config.Scope.Add("profile");
                    config.RequireHttpsMetadata = true;
                });

            return services;
        }
    }
}
