using UIApp.SignalR.Hubs;

namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static IEndpointRouteBuilder MapHubs(this IEndpointRouteBuilder app)
        {
            // passive hubs
            
            app.MapHub<NewPassiveMessageNotificationHub>("/notifications/passiveMessages")
                .RequireAuthorization();            
            app.MapHub<NewPassiveFriendrequestNotificationHub>("/notifications/passiveFriendrequests")
                .RequireAuthorization();
            app.MapHub<NewPassiveReactionNotificationHub>("/notifications/passiveReactions")
                .RequireAuthorization();
            app.MapHub<NewPassiveCommentNotificationHub>("/notifications/passiveComments")
                .RequireAuthorization();

            // active hubs

            app.MapHub<NewActiveReactionNotificationHub>("/notifications/activeReactions")
                .RequireAuthorization();
            app.MapHub<NewActivePostNotificationHub>("/notifications/activePosts")
                .RequireAuthorization(); 
            app.MapHub<NewActiveMessageNotificationHub>("/notifications/activeMessages")
                .RequireAuthorization();

            return app;
        }
    }
}
