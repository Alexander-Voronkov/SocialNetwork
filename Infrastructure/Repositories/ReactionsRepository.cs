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
    public class ReactionsRepository : IReactionsRepository
    {
        private readonly IApplicationDbContext _context;
        public ReactionsRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Reaction entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Reaction> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reaction> Find(Expression<Func<Reaction, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Reaction Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(Reaction entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Reaction> entities)
        {
            throw new NotImplementedException();
        }
    }
}
