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
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
            new ApiScope("DataApi")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               
            // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "WebUI",
                    ClientSecrets = { new Secret("WebUISecretToken".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:7054/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:7054/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:7054/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "DataApi" }
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
            await configContext.SaveChangesAsync();
        }
    }
}