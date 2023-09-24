using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task Add(User entity)
        {
            return _context.Users.AddAsync(entity).AsTask();
        }

        public Task AddRange(IEnumerable<User> entities)
        {
            return _context.Users.AddRangeAsync(entities);
        }

        public Task<IQueryable<User>> FindMany(Expression<Func<User, bool>> predicate)
        {
            return Task.FromResult(_context.Users.Where(predicate));
        }

        public Task<User?> FindOne(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.FirstOrDefaultAsync(predicate);
        }

        public Task<User?> Get(int id)
        {
            return _context.Users.FirstOrDefaultAsync(x=>x.Id == id)!;
        }

        public Task<IQueryable<User>> GetAll()
        {
            return Task.FromResult(_context.Users.AsQueryable());
        }

        public Task Remove(User entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<User> entities)
        {
            _context.RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
