using Data.DTOs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using UIApp.Services.Interfaces;

namespace UIApp.SignalR.Hubs
{
    public class NewActiveMessageNotificationHub : Hub
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly IWatchingChatCacheRepository _cache;
        public NewActiveMessageNotificationHub(
            IHttpClientFactory httpClientFactory,
            IWatchingChatCacheRepository cache)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("MessagesApi");
            _cache = cache;
        }

        public Task Send(string chat, string message)
        {
            var userId = int.Parse(Context.UserIdentifier!);
            var chatId = int.Parse(chat);

            var messageDto = new MessageDto()
            {
                MessageBody = message,
                OwnerId = userId,
                ChatId = chatId
            };

            var jsonContent = JsonConvert.SerializeObject(messageDto);

            var content = new StringContent(jsonContent, null, "application/json");

            _httpClient.PostAsync("", content);

            return Task.CompletedTask;
        }

        public Task Update(string messageId, string message)
        {
            var userId = int.Parse(Context.UserIdentifier!);

            var messageDto = new MessageDto()
            {
                MessageBody = message,
                Id = int.Parse(messageId)
            };

            var jsonContent = JsonConvert.SerializeObject(messageDto);

            var content = new StringContent(jsonContent, null, "application/json");

            _httpClient.PutAsync("", content);

            return Task.CompletedTask;
        }

        public Task Remove(string messageId)
        {
            var res = _httpClient.DeleteAsync($"{messageId}").Result;

            return Task.CompletedTask;
        }

        public Task Ack(string chatId)
        {
            _cache.SetChatWatcher(chatId, Context.UserIdentifier!);

            return Task.CompletedTask;
        }
    }
}
