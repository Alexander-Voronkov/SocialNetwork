namespace UIApp.Services.Interfaces
{
    public interface IWatchingChatCacheRepository
    {
        Task<IEnumerable<string>> GetChatWatchersByChatId(string chatId);
        Task SetChatWatcher(string chatId, string userId);
        Task RemoveChatWatcher(string chatId, string userId);
    }
}
