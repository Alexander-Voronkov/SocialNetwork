using Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var dbHost = Environment.GetEnvironmentVariable("DBAPI_HOST");
var dbPort = Environment.GetEnvironmentVariable("DBAPI_PORT");
var dbName = Environment.GetEnvironmentVariable("DBAPI_NAME");
var dbUser = Environment.GetEnvironmentVariable("DBAPI_USERID");
var dbPassword = Environment.GetEnvironmentVariable("DBAPI_PASS");

string connStr;

if (dbHost != null)
    connStr =
        $"Data Source={dbHost},{dbPort};" +
        $"Initial Catalog={dbName};" +
        $"User ID={dbUser};Password={dbPassword}";
else
    connStr =
        $"Data Source=127.0.0.1,1488;" +
        $"Initial Catalog=DataStorage;" +
        $"User ID=sa;Password=Admin_1234";

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKeys = await GetIssuerSigningKeys()
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

builder.Services
    .AddApplication()
    .AddInfrastructure(connStr);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
