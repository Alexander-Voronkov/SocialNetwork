using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using UIApp.Services.Interfaces;

namespace UIApp.SignalR.Hubs
{
    public class NewActivePostNotificationHub : Hub
    {
        private readonly IWatchingPostCacheRepository _cache;
        public NewActivePostNotificationHub(IWatchingPostCacheRepository cache)
        {
            _cache = cache;
        }

        public async Task Ack(string postIds)
        {
            var deserializedPostIds = JsonConvert.DeserializeObject<IEnumerable<string>>(postIds);
            foreach (var id in deserializedPostIds) 
            {
                await _cache.SetPostWatcher(id, Context.UserIdentifier!);
            }
        }
    }
}
