namespace UIApp.Services.Interfaces
{
    public interface IWatchingPostCacheRepository
    {
        Task<IEnumerable<string>> GetPostWatchersUserIdsByPostId(string postId);
        Task SetPostWatcher(string postId, string userId);
        Task RemovePostWatcher(string postId, string userId);
    }
}
