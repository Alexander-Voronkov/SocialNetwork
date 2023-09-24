using Data.DTOs;
using Enums;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace UIApp.SignalR.Hubs
{
    public class NewActiveReactionNotificationHub : Hub
    {
        private readonly HttpClient _client;
        private readonly IHttpClientFactory _clientFactory;
        public NewActiveReactionNotificationHub(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _client = _clientFactory.CreateClient("ReactionsApi");
        }

        public Task SendReaction(string postId, string reactionType)
        {
            var reaction = Enum.Parse<ReactionType>(reactionType);
        
            var post = int.Parse(postId);
        
            var dto = new ReactionDto()
            {
                PostId = post,
                Type = reaction
            };

            var content = new StringContent(JsonConvert.SerializeObject(dto), null, "application/json");

            _client.PostAsync("", content);
            
            return Task.CompletedTask;
        }
    }
}
