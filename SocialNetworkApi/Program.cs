using Application;

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

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7006";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "DataApi");
    });
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
