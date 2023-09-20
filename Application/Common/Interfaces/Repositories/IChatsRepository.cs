using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface IChatsRepository : IRepository<Chat>
    {
        Task<IQueryable<Chat>> GetChatWithUsers(int id);
    }
}
