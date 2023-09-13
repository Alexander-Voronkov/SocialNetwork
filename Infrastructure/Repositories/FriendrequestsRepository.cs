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
            return _context.FriendRequests.AddAsync(entity).AsTask();
        }

        public Task AddRange(IEnumerable<Friendrequest> entities)
        {
            return _context.FriendRequests.AddRangeAsync(entities);
        }

        public Task<IEnumerable<Friendrequest>> Find(Func<Friendrequest, bool> predicate)
        {
            return Task.FromResult(_context.FriendRequests.Where(predicate));
        }

        public Task<Friendrequest> Get(int id)
        {
            return _context.FriendRequests.FindAsync(id).AsTask();
        }

        public Task<IEnumerable<Friendrequest>> GetAll()
        {
            return Task.FromResult(_context.FriendRequests.AsEnumerable());
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
