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

        public Task<IQueryable<Comment>> Find(Expression<Func<Comment, bool>> predicate)
        {
            return Task.FromResult(_context.Comments.Where(predicate));
        }

        public Task<Comment> Get(int id)
        {
            return _context.Comments.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id)!;
        }

        public Task<IQueryable<Comment>> GetAll()
        {
            return Task.FromResult(_context.Comments.AsNoTracking());
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
