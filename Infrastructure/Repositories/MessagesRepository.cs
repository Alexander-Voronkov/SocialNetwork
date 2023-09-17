using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly ApplicationDbContext _context;
        public MessagesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task Add(Message entity)
        {
            return _context.Messages.AddAsync(entity).AsTask();
        }

        public Task AddRange(IEnumerable<Message> entities)
        {
            return _context.Messages.AddRangeAsync(entities);
        }

        public Task<IQueryable<Message>> Find(Func<Message, bool> predicate)
        {
            return Task.FromResult(_context.Messages.Where(predicate).AsQueryable());
        }

        public Task<Message> Get(int id)
        {
            return _context.Messages.FindAsync(id).AsTask();
        }

        public Task<IQueryable<Message>> GetAll()
        {
            return Task.FromResult(_context.Messages.AsQueryable());
        }

        public Task Remove(Message entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<Message> entities)
        {
            _context.RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
