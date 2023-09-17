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

        public Task<IQueryable<Chat>> Find(Func<Chat, bool> predicate)
        {
            return Task.FromResult(_context.Chats.Where(predicate).AsQueryable());
        }

        public Task<Chat> Get(int id)
        {
            return _context.Chats.FindAsync(id).AsTask();
        }

        public Task<IQueryable<Chat>> GetAll()
        {
            return Task.FromResult(_context.Chats.AsQueryable());
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
