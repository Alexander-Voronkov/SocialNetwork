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
                .Where(x=>x.Id == id));
        }

        public Task<Chat?> FindOne(Expression<Func<Chat, bool>> predicate)
        {
            return _context.Chats.FirstOrDefaultAsync(predicate);
        }

        public Task<Chat?> GetChatWithUsersAndMessages(int id)
        {
            return _context.Chats
                .Include(x => x.FirstUser)
                .Include(x => x.SecondUser)
                .Include(x=>x.Messages)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public Task<IQueryable<Chat>> FindMany(Expression<Func<Chat, bool>> predicate)
        {
            return Task.FromResult(_context.Chats.Where(predicate));
        }

        public Task<Chat?> Get(int id)
        {
            return _context.Chats.FirstOrDefaultAsync(x => x.Id == id)!;
        }

        public Task<IQueryable<Chat>> GetAll()
        {
            return Task.FromResult(_context.Chats.AsQueryable());
        }
    }
}
