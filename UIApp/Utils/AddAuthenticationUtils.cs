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
            var authapihost = Environment.GetEnvironmentVariable("AUTH_API_HOST") ?? "localhost";
            var authapicontainername = Environment.GetEnvironmentVariable("AUTH_API_CONTAINER_NAME") ?? "localhost";
            var authapiport = Environment.GetEnvironmentVariable("AUTH_API_PORT") ?? "7006";
            var webapihost = Environment.GetEnvironmentVariable("WEB_API_HOST") ?? "localhost";
            var webapiport = Environment.GetEnvironmentVariable("WEB_API_PORT") ?? "7129";

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
                        await client.GetAsync($"{protocol}://{webapihost}:{webapiport}/authenticate");
                    };
                    options.Events.OnSigningOut = async e =>
                    {
                        await e.HttpContext.RevokeUserRefreshTokenAsync();
                    };
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, config =>
                {
                    config.Authority = $"{protocol}://{authapicontainername}:{authapiport}";
                    config.Events = new OpenIdConnectEvents
                    {
                        OnRedirectToIdentityProvider = context =>
                        {
                            context.ProtocolMessage.IssuerAddress = $"{protocol}://{authapihost}:{authapiport}/connect/authorize";
                            return Task.CompletedTask;
                        }
                    };
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
                    if (protocol == "https")
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
