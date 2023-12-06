using SocialNetworkApi.Utils;
using System.Net;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

app.UseHealthChecks("/health");

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddlewares();

app.MapControllers()
    .RequireAuthorization();

app.MapGet("/authenticate", x => x.Response.WriteAsync(""))
    .RequireAuthorization();

app.Run();