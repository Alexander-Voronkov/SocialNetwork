using IdentityModel.Client;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Shared;
using Application;

namespace SocialNetworkApi.Utils
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddAuthenticationWithIntrospection(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(async options =>
                {
                    string authority = "https://localhost:7006";
                    string validIssuer = "https://localhost:7006";
                    string validAudience = "DataApi";

                    options.Authority = authority;
                    options.Audience = validAudience;

                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidAudience = validAudience,
                        ValidIssuer = validIssuer,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKeys = await GetIssuerSigningKeys(),
                    };

                    options.Events.OnTokenValidated = async context =>
                    {
                        var dbcontext = context.HttpContext
                                               .RequestServices
                                               .GetRequiredService<ApplicationDbContext>();
                        var client = new HttpClient();
                        var token = await context.HttpContext.GetTokenAsync("access_token");
                        var userinfo = await client.GetUserInfoAsync(new UserInfoRequest()
                        {
                            Token = token,
                            Address = "https://localhost:7006/"
                        });
                    };

                    async Task<IEnumerable<SecurityKey>> GetIssuerSigningKeys()
                    {
                        HttpClient client = new HttpClient();
                        var metadataRequest = new HttpRequestMessage(HttpMethod.Get, $"{authority}/.well-known/openid-configuration");
                        var metadataResponse = await client.SendAsync(metadataRequest);

                        string content = await metadataResponse.Content.ReadAsStringAsync();

                        var payload = JObject.Parse(content);
                        string jwksUri = payload.Value<string>("jwks_uri")!;

                        var keysRequest = new HttpRequestMessage(HttpMethod.Get, jwksUri);
                        var keysResponse = await client.SendAsync(keysRequest);
                        var keysPayload = await keysResponse.Content.ReadAsStringAsync();
                        var signingKeys = new JsonWebKeySet(keysPayload).Keys;
                        return signingKeys;
                    }
                });

            return services;
        }

        public static IServiceCollection AddLayersAndConfigure(this IServiceCollection services)
        {
            var dbHost = Environment.GetEnvironmentVariable("DBAPI_HOST") ?? "127.0.0.1";
            var dbPort = Environment.GetEnvironmentVariable("DBAPI_PORT") ?? "1433";
            var dbName = Environment.GetEnvironmentVariable("DBAPI_NAME") ?? "SOCIALNETWORK";
            var dbUser = Environment.GetEnvironmentVariable("DBAPI_USERID") ?? "sa";
            var dbPassword = Environment.GetEnvironmentVariable("DBAPI_PASS") ?? "Admin_1234";
            var rabbitHost = Environment.GetEnvironmentVariable("RabbitMQHost") ?? "127.0.0.1";
            var rabbitPort = Environment.GetEnvironmentVariable("RabbitMQPort") ?? "5672";
            var identityDbHost = Environment.GetEnvironmentVariable("IDENTITY_HOST") ?? "127.0.0.1";
            var identityDbPort = Environment.GetEnvironmentVariable("IDENTITY_PORT") ?? "1422";
            var identityDbName = Environment.GetEnvironmentVariable("IDENTITY_NAME") ?? "USERS_IDENTITY_DB";
            var identityDbUser = Environment.GetEnvironmentVariable("IDENTITY_USERID") ?? "sa";
            var identityDbPassword = Environment.GetEnvironmentVariable("IDENTITY_PASS") ?? "Admin_1234";

            string connStr =
                    $"Data Source={dbHost},{dbPort};" +
                    $"Initial Catalog={dbName};" +
                    $"User ID={dbUser};Password={dbPassword}";

            string identityConnStr =
                    $"Data Source={identityDbHost},{identityDbPort};" +
                    $"Initial Catalog={identityDbName};" +
                    $"User ID={identityDbUser};Password={identityDbPassword}";
            
            services.Configure<RabbitMQConfiguration>(options =>
            {
                options.Host = rabbitHost;
                options.Port = int.Parse(rabbitPort!);
            });


            services
                .AddApplication()
                .AddInfrastructure(connStr);

            return services;
        }
    }
}
