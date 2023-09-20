﻿
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public Task<IQueryable<Post>> Find(Expression<Func<Post, bool>> predicate)
        {
            return Task.FromResult(_context.Posts.Where(predicate));
        }

        public Task<Post> Get(int id)
        {
            return _context.Posts.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id)!;
        }

        public Task<IQueryable<Post>> GetAll()
        {
            return Task.FromResult(_context.Posts.AsNoTracking());
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
