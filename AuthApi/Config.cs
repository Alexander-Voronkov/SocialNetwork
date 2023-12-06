using AuthApi.Data;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthApi
{
    public static class Config
    {
        private static string nginxprotocol = Environment.GetEnvironmentVariable("NGINX_PROTOCOL") ?? "http";
        private static string webuihost = Environment.GetEnvironmentVariable("WEB_UI_HOST") ?? "localhost";
        private static string webuinginxport = Environment.GetEnvironmentVariable("WEB_UI_PORT") ?? "7054";
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
                    RedirectUris = { $"{nginxprotocol}://{webuihost}:{webuinginxport}/signin-oidc" },
                    FrontChannelLogoutUri = $"{nginxprotocol}://{webuihost}:{webuinginxport}/signout-oidc",
                    PostLogoutRedirectUris = { $"{nginxprotocol}://{webuihost}:{webuinginxport}/signout-callback-oidc" },
                    AllowedScopes = { "openid", "profile", "phone", "email", "DataApi:read" },
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                },
            };

        public static async Task InitIdentityServerConfiguration(this WebApplication app)
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