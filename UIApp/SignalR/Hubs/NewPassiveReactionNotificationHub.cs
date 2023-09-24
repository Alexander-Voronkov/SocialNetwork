using Microsoft.AspNetCore.SignalR;

namespace UIApp.SignalR.Hubs
{
    public class NewPassiveReactionNotificationHub : Hub
    {
        public NewPassiveReactionNotificationHub()
        {

        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
