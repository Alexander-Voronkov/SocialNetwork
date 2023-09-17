using IdentityModel.Client;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Shared;
using Application;
using System.Net.Http;

namespace SocialNetworkApi.Utils
{
    public static partial class ServicesExtensions
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

        
    }
}
