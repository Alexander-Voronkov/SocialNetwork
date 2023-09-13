using UIApp.SignalR.Hubs;

namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static IEndpointRouteBuilder MapHubs(this IEndpointRouteBuilder app)
        {
            app.MapHub<ChatHub>("/chat");
            app.MapHub<RabbitConsumerHub>("/notifications");

            return app;
        }
    }
}
