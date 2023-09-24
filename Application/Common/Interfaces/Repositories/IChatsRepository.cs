using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface IChatsRepository : IRepository<Chat>
    {
        Task<IQueryable<Chat>> GetChatWithUsers(int id);
        Task<Chat?> GetChatWithUsersAndMessages(int id);
    }
}
