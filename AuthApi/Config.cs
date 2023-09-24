using AuthApi.Data;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthApi
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
            };
        public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource()
            {
                Name = "DataApi", 
                DisplayName = "Data Api",
                Scopes = { "DataApi:read" },
            }
        };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope()
                {
                    Name = "DataApi:read"
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "WebUI",
                    ClientSecrets = { new Secret("WebUISecretToken".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { $"https://{(Environment.GetEnvironmentVariable("WEB_UI_HOST") ?? "localhost")}:{(Environment.GetEnvironmentVariable("WebUIPort") ?? "7054")}/signin-oidc" },
                    FrontChannelLogoutUri = $"https://{(Environment.GetEnvironmentVariable("WEB_UI_HOST") ?? "localhost")}:{(Environment.GetEnvironmentVariable("WebUIPort") ?? "7054")}/signout-oidc",
                    PostLogoutRedirectUris = { $"https://{(Environment.GetEnvironmentVariable("WEB_UI_HOST") ?? "localhost")}:{(Environment.GetEnvironmentVariable("WebUIPort") ?? "7054")}/signout-callback-oidc" },
                    AllowedScopes = { "openid", "profile", "phone", "email", "DataApi:read" },
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Sliding,                   
                },
            };

        public static async void InitIdentityServerConfiguration(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var persistContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
            var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var configContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            await configContext.Database.MigrateAsync();
            await persistContext.Database.MigrateAsync();
            await appContext.Database.MigrateAsync();
            if (!await configContext.Clients.AnyAsync())
                await configContext.Clients.AddRangeAsync(Config.Clients.Select(x=>x.ToEntity()));
            if (!await configContext.ApiScopes.AnyAsync())
                await configContext.ApiScopes.AddRangeAsync(Config.ApiScopes.Select(x => x.ToEntity()));
            if (!await configContext.IdentityResources.AnyAsync())
                await configContext.IdentityResources.AddRangeAsync(Config.IdentityResources.Select(x => x.ToEntity()));
            if (!await configContext.ApiResources.AnyAsync())
                await configContext.ApiResources.AddRangeAsync(Config.ApiResources.Select(x => x.ToEntity()));
            await configContext.SaveChangesAsync();
        }
    }
}