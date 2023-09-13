using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Shared;
using System.Xml.Linq;
using UIApp.Services.Interfaces;
using UIApp.Services.Realizations;
using UIApp.SignalR.Hubs;
using UIApp.Utils;

var builder = WebApplication.CreateBuilder(args);

// hangfire background jobs database connection string

var dbHangfireHost = Environment.GetEnvironmentVariable("DB_HANGFIRE_HOST") ?? "127.0.0.1";
var dbHangfirePort = Environment.GetEnvironmentVariable("DB_HANGFIRE_PORT") ?? "1444";
var dbHangfireName = Environment.GetEnvironmentVariable("DB_HANGFIRE_NAME") ?? "DB_HANGFIRE_JOBS";
var dbHangfireUser = Environment.GetEnvironmentVariable("DB_HANGFIRE_USERID") ?? "sa";
var dbHangfirePassword = Environment.GetEnvironmentVariable("DB_HANGFIRE_PASS") ?? "Admin_1234";

var sqlbuilder = new SqlConnectionStringBuilder();
sqlbuilder.DataSource = $"{dbHangfireHost},{dbHangfirePort}";
sqlbuilder.InitialCatalog = $"{dbHangfireName}";
sqlbuilder.UserID = $"{dbHangfireUser}";
sqlbuilder.Password = $"{dbHangfirePassword}";
sqlbuilder.Encrypt = true;
sqlbuilder.TrustServerCertificate = true;

string connStr = sqlbuilder.ConnectionString;

// rabbit mq connection info

builder.Services.Configure<RabbitMQConfiguration>(options =>
{
    options.Host = Environment.GetEnvironmentVariable("RabbitMQHost") ?? "127.0.0.1";
    string? port = Environment.GetEnvironmentVariable("RabbitMQPort");
    options.Port = port == null ? 5672 : int.Parse(port);
    options.Username = Environment.GetEnvironmentVariable("RabbitMQUser") ?? "ADMIN";
    options.Password = Environment.GetEnvironmentVariable("RabbitMQPass") ?? "Admin_1234";
});

builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddHangfire(config =>
{
    config
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(connStr);
});

builder.Services.AddHangfireServer();

builder.Services.AddTransient<IRabbitQueueConsumer, RabbitQueueConsumer>();

builder.Services
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
    });

builder.Services.AddHttpClient("DataApi", (services, client) =>
{
    client.BaseAddress = new Uri("https://localhost:7129");
});

var app = builder.Build();

await Utils.InitHangfireDb(sqlbuilder);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<ChatHub>("/chat");

app.MapHub<RabbitConsumerHub>("/notifications");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHangfireDashboard();

app.MapHangfireDashboard();

Utils.AddJobs();

app.Run();
