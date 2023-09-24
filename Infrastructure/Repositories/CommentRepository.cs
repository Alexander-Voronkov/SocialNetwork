using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task Add(Comment entity)
        {
            return _context.Comments.AddAsync(entity).AsTask();
        }

        public Task AddRange(IEnumerable<Comment> entities)
        {
            return _context.Comments.AddRangeAsync(entities);
        }

        public Task<Comment?> FindOne(Expression<Func<Comment, bool>> predicate)
        {
            return _context.Comments.FirstOrDefaultAsync(predicate);
        }

        public Task<IQueryable<Comment>> FindMany(Expression<Func<Comment, bool>> predicate)
        {
            return Task.FromResult(_context.Comments.Where(predicate));
        }

        public Task<Comment?> Get(int id)
        {
            return _context.Comments.FirstOrDefaultAsync(x=>x.Id == id)!;
        }

        public Task<IQueryable<Comment>> GetAll()
        {
            return Task.FromResult(_context.Comments.AsQueryable());
        }

        public Task Remove(Comment entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<Comment> entities)
        {
            _context.RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
