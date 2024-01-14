using Serilog;
using SocialNetworkApi.Utils;
using System.Net;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, logger) =>
{
    var protocol = Environment.GetEnvironmentVariable("PROTOCOL") ?? "http";
    var serverUrl = Environment.GetEnvironmentVariable("SEQ_CONTAINER_NAME") ?? "localhost";
    logger.ReadFrom.Configuration(context.Configuration);
    logger.WriteTo.Seq($"{protocol}://{serverUrl}:5341");
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddServices();

builder.Services.AddControllers()
    .AddJsonOptions(x=>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddRabbitMqConfiguration();

builder.Services.AddAuthenticationWithIntrospection();

builder.Services.AddHttpContextAccessor();

builder.Services.AddLayersAndConfigure();

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

app.UseSerilogRequestLogging();

app.UseHealthChecks("/health");

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddlewares();

app.MapControllers()
    .RequireAuthorization();

app.MapGet("/authenticate", x => x.Response.WriteAsync(""))
    .RequireAuthorization();

app.Run();