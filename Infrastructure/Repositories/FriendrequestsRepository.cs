using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class FriendrequestsRepository : IFriendrequestsRepository
    {
        private readonly ApplicationDbContext _context;
        public FriendrequestsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task Add(Friendrequest entity)
        {
            return _context.Friendrequests.AddAsync(entity).AsTask();
        }

        public Task AddRange(IEnumerable<Friendrequest> entities)
        {
            return _context.Friendrequests.AddRangeAsync(entities);
        }

        public Task<IQueryable<Friendrequest>> FindMany(Expression<Func<Friendrequest, bool>> predicate)
        {
            return Task.FromResult(_context.Friendrequests.Where(predicate));
        }

        public Task<Friendrequest?> FindOne(Expression<Func<Friendrequest, bool>> predicate)
        {
            return _context.Friendrequests.FirstOrDefaultAsync(predicate);
        }

        public Task<Friendrequest?> Get(int id)
        {
            return _context.Friendrequests.FirstOrDefaultAsync(x=>x.Id == id)!;
        }

        public Task<IQueryable<Friendrequest>> GetAll()
        {
            return Task.FromResult(_context.Friendrequests.AsQueryable());
        }

        public Task Remove(Friendrequest entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<Friendrequest> entities)
        {
            _context.RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
