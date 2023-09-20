using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public Task<IQueryable<Message>> Find(Expression<Func<Message, bool>> predicate)
        {
            return Task.FromResult(_context.Messages.Where(predicate));
        }

        public Task<Message> Get(int id)
        {
            return _context.Messages.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id)!;
        }

        public Task<IQueryable<Message>> GetAll()
        {
            return Task.FromResult(_context.Messages.AsNoTracking());
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
