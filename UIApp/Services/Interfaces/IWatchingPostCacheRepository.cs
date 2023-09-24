namespace UIApp.Services.Interfaces
{
    public interface IWatchingPostCacheRepository
    {
        Task<IEnumerable<string>> GetPostWatchersByPostId(string postId);
        Task SetPostWatcher(string postId, string userId);
        Task RemovePostWatcher(string postId, string userId);
    }
}
