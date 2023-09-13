using Application;
using Application.Common.Interfaces;
using IdentityModel.Client;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Shared;
using SocialNetworkApi.Services;
using SocialNetworkApi.Utils;
using System.Text.Json.Nodes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddControllers();

builder.Services.AddAuthenticationWithIntrospection();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IUser, CurrentUser>();

builder.Services.AddLayersAndConfigure();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers()
    .RequireAuthorization();

app.Run();
