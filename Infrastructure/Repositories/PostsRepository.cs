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
    public class PostsRepository : IPostsRepository
    {
        private readonly IApplicationDbContext _context;
        public PostsRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Post entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Post> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> Find(Expression<Func<Post, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Post Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(Post entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Post> entities)
        {
            throw new NotImplementedException();
        }
    }
}
