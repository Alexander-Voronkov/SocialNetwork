using Hangfire;
using UIApp.Services.Interfaces;
using UIApp.Services.Realizations;
using UIApp.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

await builder.Services.AddHangfireAndConfigure();

builder.Services.AddHangfireServer();

builder.Services.AddTransient<IRabbitQueueConsumer, RabbitQueueConsumer>();

builder.Services.AddAuthenticationUtils();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapHubs();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHangfireDashboard();

app.MapHangfireDashboard();

Utils.AddJobs();

app.Run();
