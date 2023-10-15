using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using UIApp.Services.Interfaces;

namespace UIApp.Services.Realizations
{
    public class WatchingPostCache : IWatchingPostCacheRepository
    {
        private readonly IDistributedCache _cache;
        public WatchingPostCache(IDistributedCache distributedCache)
        {
            _cache = distributedCache;
        }

        public async Task SetPostWatcher(string postId, string userId)
        {
            var currentUsers = await _cache.GetStringAsync($"post-{postId}");

            List<string> users;

            if (currentUsers == null)
            {
                users = new List<string>() { userId };
            }
            else
            {
                users = JsonConvert.DeserializeObject<List<string>>(currentUsers);
                if(!users.Contains(userId))
                    users.Add(userId);
            }

            await _cache.SetStringAsync($"post-{postId}",
                JsonConvert.SerializeObject(users),
                new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                });
        }

        public async Task<IEnumerable<string>> GetPostWatchersUserIdsByPostId(string postId)
        {
            var jsonUserIds = await _cache.GetStringAsync($"post-{postId}");
            if(jsonUserIds == null)
            {
                return Array.Empty<string>();
            }
            var deserializedUserIds = JsonConvert.DeserializeObject<IEnumerable<string>>(jsonUserIds);
            return deserializedUserIds;
        }

        public async Task RemovePostWatcher(string postId, string connectionId)
        {
            var currentUsers = await _cache.GetStringAsync($"post-{postId}");
            if (currentUsers != null)
            {
                var connections = JsonConvert.DeserializeObject<List<string>>(currentUsers);
                if (!connections.Contains(connectionId))
                    return;
                connections.Remove(connectionId);
                await _cache.SetStringAsync($"post-{postId}", JsonConvert.SerializeObject(connections),
                new DistributedCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                });
            }
        }
    }
}
