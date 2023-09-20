using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public Task<IQueryable<Chat>> Find(Expression<Func<Chat, bool>> predicate)
        {
            return Task.FromResult(_context.Chats.Where(predicate));
        }

        public Task<Chat> Get(int id)
        {
            return _context.Chats.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id)!;
        }

        public Task<IQueryable<Chat>> GetAll()
        {
            return Task.FromResult(_context.Chats.AsNoTracking());
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

        public Task<IQueryable<Chat>> GetChatWithUsers(int id)
        {
            return Task.FromResult(_context.Chats
                .Include(x => x.FirstUser)
                .Include(x => x.SecondUser)
                .AsNoTracking().Where(x=>x.Id == id));
        }
    }
}
