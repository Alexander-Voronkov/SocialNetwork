using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Net;
using System.Reflection;
using UIApp.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, logger) =>
{
    var protocol = Environment.GetEnvironmentVariable("PROTOCOL") ?? "http";
    var serverUrl = Environment.GetEnvironmentVariable("SEQ_CONTAINER_NAME") ?? "localhost";
    logger.ReadFrom.Configuration(context.Configuration);
    logger.WriteTo.Seq($"{protocol}://{serverUrl}:5341");
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthenticationUtils();

builder.Services.AddServices();

builder.Services.AddHttpClients();

builder.Services.AddSignalR();

builder.Services.AddRedisConfiguration();

builder.Services.AddRabbitMqConfigurations();

builder.Services.AddHangfireAndConfiguration();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddConsumers();

builder.Services.AddHealthChecks();

builder.Services.AddCors();

var app = builder.Build();

var protocol = Environment.GetEnvironmentVariable("PROTOCOL") ?? "http";

if (protocol == "http")
{
    app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
}

if (protocol == "https")
{
    app.UseHttpsRedirection();
}

app.UseCors(builder =>
{
    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});

app.UseSerilogRequestLogging();

app.UseHealthChecks("/health");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapHubs();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
        .RequireAuthorization();

app.UseHangfireDashboard();

app.MapHangfireDashboard()
    .RequireAuthorization();

Utils.AddJobs();

app.Run();
