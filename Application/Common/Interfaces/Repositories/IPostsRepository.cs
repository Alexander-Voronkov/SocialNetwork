using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface IPostsRepository : IRepository<Post>
    {
        Task<Post?> GetPostWithReactions(int id);
    }
}
