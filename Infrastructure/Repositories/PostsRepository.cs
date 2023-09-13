using Application.Common.Interfaces;
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
    public class PostsRepository : IPostsRepository
    {
        private readonly ApplicationDbContext _context;
        public PostsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task Add(Post entity)
        {
            return _context.Posts.AddAsync(entity).AsTask();
        }

        public Task AddRange(IEnumerable<Post> entities)
        {
            return _context.Posts.AddRangeAsync(entities);
        }

        public Task<IEnumerable<Post>> Find(Func<Post, bool> predicate)
        {
            return Task.FromResult(_context.Posts.Where(predicate));
        }

        public Task<Post> Get(int id)
        {
            return _context.Posts.FindAsync(id).AsTask()!;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts.ToArrayAsync();
        }

        public Task Remove(Post entity)
        {
            _context.Posts.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<Post> entities)
        {
            _context.Posts.RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
