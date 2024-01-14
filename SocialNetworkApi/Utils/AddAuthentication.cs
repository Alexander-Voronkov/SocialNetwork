using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace SocialNetworkApi.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddAuthenticationWithIntrospection(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(async options =>
                {
                    string protocol = Environment.GetEnvironmentVariable("PROTOCOL") ?? "http";
                    string issuer = $"{protocol}://{Environment.GetEnvironmentVariable("AUTH_API_HOST") ?? "localhost"}:{Environment.GetEnvironmentVariable("AUTH_API_PORT") ?? "7006"}";
                    string validAudience = "DataApi";

                    options.Authority = issuer;
                    options.Audience = validAudience;

                    if (protocol == "https")
                    {
                        options.RequireHttpsMetadata = true;
                    }
                    else
                    {
                        options.RequireHttpsMetadata = false;
                    }

                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidAudience = validAudience,
                        ValidIssuer = issuer,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKeys = await GetIssuerSigningKeys(),
                    };

                    async Task<IEnumerable<SecurityKey>> GetIssuerSigningKeys()
                    {
                        HttpClient client = new HttpClient();
                        var metadataRequest = new HttpRequestMessage(HttpMethod.Get, $"{issuer}/.well-known/openid-configuration");
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
