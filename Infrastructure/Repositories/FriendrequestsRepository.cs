using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public Task<IQueryable<Friendrequest>> Find(Func<Friendrequest, bool> predicate)
        {
            return Task.FromResult(_context.Friendrequests.Where(predicate).AsQueryable());
        }

        public Task<Friendrequest> Get(int id)
        {
            return _context.Friendrequests.FindAsync(id).AsTask();
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
