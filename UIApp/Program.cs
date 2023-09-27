using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using UIApp.Utils;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

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
