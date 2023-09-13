using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ChatsRepository : IChatsRepository
    {
        private readonly ApplicationDbContext _context;
        public ChatsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task Add(Chat entity)
        {
            return _context.Chats.AddAsync(entity).AsTask();
        }

        public Task AddRange(IEnumerable<Chat> entities)
        {
            return _context.Chats.AddRangeAsync(entities);
        }

        public Task<IEnumerable<Chat>> Find(Func<Chat, bool> predicate)
        {
            return Task.FromResult(_context.Chats.Where(predicate));
        }

        public Task<Chat> Get(int id)
        {
            return _context.Chats.FindAsync(id).AsTask();
        }

        public Task<IEnumerable<Chat>> GetAll()
        {
            return Task.FromResult(_context.Chats.AsEnumerable());
        }

        public Task Remove(Chat entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<Chat> entities)
        {
            _context.RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
