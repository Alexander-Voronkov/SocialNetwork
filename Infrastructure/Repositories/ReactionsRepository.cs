using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ReactionsRepository : IReactionsRepository
    {
        private readonly ApplicationDbContext _context;
        public ReactionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task Add(Reaction entity)
        {
            return _context.Reactions.AddAsync(entity).AsTask();
        }

        public Task AddRange(IEnumerable<Reaction> entities)
        {
            return _context.Reactions.AddRangeAsync(entities);
        }

        public Task<IQueryable<Reaction>> FindMany(Expression<Func<Reaction, bool>> predicate)
        {
            return Task.FromResult(_context.Reactions.Where(predicate));
        }

        public Task<Reaction?> FindOne(Expression<Func<Reaction, bool>> predicate)
        {
            return _context.Reactions.FirstOrDefaultAsync(predicate);
        }

        public Task<Reaction?> Get(int id)
        {
            return _context.Reactions.FirstOrDefaultAsync(x=>x.Id == id)!;
        }

        public Task<IQueryable<Reaction>> GetAll()
        {
            return Task.FromResult(_context.Reactions.AsQueryable());
        }

        public Task Remove(Reaction entity)
        {
            _context.Reactions.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<Reaction> entities)
        {
            _context.Reactions.RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
