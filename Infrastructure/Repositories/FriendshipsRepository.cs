using Application.Common.Interfaces;
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
    public class FriendshipsRepository : IFriendshipsRepository
    {
        private readonly IApplicationDbContext _context;
        public FriendshipsRepository(IApplicationDbContext context) 
        { 
            _context = context;
        }
        public void Add(Friendship entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Friendship> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Friendship> Find(Expression<Func<Friendship, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Friendship Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Friendship> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(Friendship entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Friendship> entities)
        {
            throw new NotImplementedException();
        }
    }
}
