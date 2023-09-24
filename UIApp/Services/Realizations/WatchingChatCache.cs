using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using UIApp.Services.Interfaces;

namespace UIApp.Services.Realizations
{
    public class WatchingChatCache : IWatchingChatCacheRepository
    {
        private readonly IDistributedCache _cache;
        public WatchingChatCache(IDistributedCache cache) 
        {
            _cache = cache;
        }
        public async Task<IEnumerable<string>> GetChatWatchersByChatId(string chatId)
        {
            var jsonUserIds = await _cache.GetStringAsync($"chat-{chatId}");
            var deserializedUserIds = JsonConvert.DeserializeObject<List<string>>(jsonUserIds);
            return deserializedUserIds;
        }

        public async Task SetChatWatcher(string chatId, string userId)
        {
            var currentUsers = await _cache.GetStringAsync($"chat-{chatId}");

            List<string> users;

            if(currentUsers == null)
            {
                users = new List<string>() { userId };
            }
            else
            {
                users = JsonConvert.DeserializeObject<List<string>>(currentUsers);
                if(!users.Contains(userId))
                    users.Add(userId);
            }

            await _cache.SetStringAsync($"chat-{chatId}", 
                JsonConvert.SerializeObject(users), 
                new DistributedCacheEntryOptions() 
                { 
                    SlidingExpiration = TimeSpan.FromMinutes(30) 
                });
        }

        public async Task RemoveChatWatcher(string chatId, string userId)
        {
            var currentUsers = await _cache.GetStringAsync($"chat-{chatId}");
            if(currentUsers != null)
            {
                var users = JsonConvert.DeserializeObject<List<string>>(currentUsers);
                users.Remove(userId);
                await _cache.SetStringAsync($"chat-{chatId}", JsonConvert.SerializeObject(users));
            }
        }
    }
}
